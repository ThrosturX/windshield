using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Windshield.Models;
using Windshield.Models.Games.Common.Ratings;


namespace Windshield.Controllers
{
	[Authorize]
	public class GameHub : Hub
	{
		public enum GamesTypeIDs
		{
			TicTacToe = 2
		}

		private IBoardRepo boardRepository = new BoardRepo();
		private IUserRepo userRepository = new UserRepo();
		private IGameRepo gameRepository = new GameRepo();

		public void Join(string groupId)
		{
			//			Context.User.Identity.Name
			// Context.QueryString

			Groups.Add(Context.ConnectionId, groupId);
		}

		public void RefreshLobby(string boardID)
		{
			Clients.Group(boardID).UpdateList();
		}

		public void send(string groupName, string user, string message)
		{
			if (message.Length > 0 && user.Length > 0)
			{
				Clients.Group(groupName).addMessage("<span class=\"chatusername\">" + user + ":</span><br>" + message);
			}

		}

		public void ClickCell(string groupName, string cellId)
		{
			Clients.OthersInGroup(groupName).cellClicked(cellId);
			//			Clients.Others.cellClicked(cellId);
		}

		public void GameStarted(string boardID)
		{
			Clients.OthersInGroup(boardID).start(boardID);
		}

		public void Refresh(string boardID)
		{
			int id;
			int.TryParse(boardID, out id);
			Board board = boardRepository.GetBoardById(id);

			string status = board.status;

			Clients.Group(id.ToString()).Broadcast(status);
		}

		public void TryAction(string boardID, string action, string sender)
		{
			int id;
			int.TryParse(boardID, out id);
			Board board = boardRepository.GetBoardById(id);

			string status = board.status;

			var ttt = new Windshield.Models.Games.TicTacToe.TicTacToe(ExtractUsers((int)GamesTypeIDs.TicTacToe, status));
			ttt.SetStatus(board.status);

			int response = ttt.TryAction(action, sender);

			switch(response)
			{
				case 1:
					{
						string message = ttt.GetStatus();
						Clients.Group(id.ToString()).Broadcast(message);
						board.status = message;
						boardRepository.Save();
						break;
					}
				case 2:
					{
						Clients.Group(id.ToString()).Broadcast(ttt.GetStatus());

						// Begin update ratings

						IQueryable<User> players;
						List<Elo> ratings = new List<Elo>();
						User outlier;
						Elo elo;
						GameRating rating;
						int outlierScore;
						string winner;
						players = boardRepository.GetBoardUsers(board);

						winner = ttt.GetGameOver();
						if (winner != "" && winner != "Computer") // don't let the computer win and take elo :)
						{
							outlier = userRepository.GetUserByName(winner);
							rating = userRepository.GetGameRatingByGame(outlier, gameRepository.GetGameByID(2)); // TODO: late binding (2 = tictactoe id)
							if (rating == null)
							{
								rating.rating = 0;
							}

							outlierScore = rating.rating;
							elo = new Elo(2, outlier); // TODO: late binding (2 = tictactoe id)
							if (outlierScore != 0)
							{
								elo.points = outlierScore;
							}
							foreach (var user in players)
							{
								if (user != outlier)
								{
									Elo tempElo = new Elo(2, user); // TODO LATE BINDING
									GameRating trating = userRepository.GetGameRatingByGame(user, gameRepository.GetGameByID(2)); // TODO LATE BINDING
									if (rating.rating != 0)
									{
										tempElo.points = trating.rating;
									}
									ratings.Add(tempElo);
								}
							}

							elo.UpdateAll(1, ratings);

							rating.rating = elo.points;

							foreach (var r in ratings)
							{
								GameRating gr = userRepository.GetGameRatingByGame(r.user, gameRepository.GetGameByID(2)); // TODO LATE BINDING
								gr.rating = r.points;
							}

							userRepository.Save();
						}

						// End update ratings

						Clients.Group(id.ToString()).GameOver(winner);
						string message = ttt.GetStatus();
						//Clients.Group(id.ToString()).Broadcast(message);
						board.status = message;
						boardRepository.Save();
						break;
					}

				case 0:
				default:
					{
						// intentionally left blank
						break;
					}
			}

		}

		public List<User> ExtractUsers(int gameTypeID, string status)
		{
			List<User> users = new List<User>();

			string [] info = status.Split('|');

			switch (gameTypeID)
			{
				case 2:
					{
						User p1 = userRepository.GetUserByName(info[6]);
						users.Add(p1);

						if (info[7] != "Computer")
						{
							User p2 = userRepository.GetUserByName(info[7]);
							users.Add(p2);
						}
						break;
					}
				default:
					return null;
			}

			return users;
		}

	}
}
