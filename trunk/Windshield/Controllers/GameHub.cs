using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Windshield.Models;
using Windshield.Models.Games;
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

		public void StartGame(string boardID)
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

			IGame iGame = IGameBinder.GetGameObjectFor(board.Game.model);

			string status = board.status;
			iGame.AddPlayers(boardRepository.GetBoardUsers(board).ToList());
			iGame.SetStatus(board.status);

			int response = iGame.TryAction(action, sender);

			switch(response)
			{
				case 1:
					{
						string message = iGame.GetStatus();
						Clients.Group(id.ToString()).Broadcast(message);
						board.status = message;
						boardRepository.Save();
						break;
					}
				case 2:  // Game win, calculate Elo
					{
						//Send status string to all clients
						Clients.Group(id.ToString()).Broadcast(iGame.GetStatus());

						var winner = iGame.GetGameOver();
						/*Change elo points for all players on the board. -10 for loosers +10 for winner. */
						
						//Gets all users from board.
						List<User> listUser = new List<User>();
						listUser = boardRepository.GetBoardUsers(board).ToList();
						
						foreach (var player in listUser)
						{
							//Fetching GameRating from database for player.
							GameRating rating;
							rating = gameRepository.GetGameRatingByNameAndGameID(player.UserName, board.idGame);
							
							if (player.UserName == winner)
							{
								rating.rating = rating.rating + 10;
							}
							else 
							{
								rating.rating = rating.rating - 10;					
							} 
							
							gameRepository.Save();
						}

						//Save in Database.
						
						userRepository.Save();
						//Restartbutton
						Clients.Group(id.ToString()).GameOver(winner);
						string message = iGame.GetStatus();
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
		}  // public void TryAction(string boardID, string action, string sender)

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

						if(!info[7].StartsWith("Computer"))
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
