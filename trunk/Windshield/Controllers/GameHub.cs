using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Windshield.Models;


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

		public void Join(string groupId)
		{
			//			Context.User.Identity.Name
			// Context.QueryString

			Groups.Add(Context.ConnectionId, groupId);
		}

		public void send(string groupName, string user, string message)
		{
			if (message.Length > 0 && user.Length > 0)
			{
				Clients.Group(groupName).addMessage("<span class=\"chatusername\">" + user + "</span> &gt; " + message);
			}

		}

		public void ClickCell(string groupName, string cellId)
		{
			Clients.OthersInGroup(groupName).cellClicked(cellId);
			//			Clients.Others.cellClicked(cellId);
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
						Clients.Group(id.ToString()).GameOver(ttt.GetGameOver());
						string message = ttt.GetStatus();
						Clients.Group(id.ToString()).Broadcast(message);
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
