using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Windshield.Models;


namespace Windshield.Controllers
{
//	[Authorize]
	public class GameHub : Hub
	{
		private IBoardRepo boardRepository = new BoardRepo();

		public void Join(string groupId)
		{
			//			Context.User.Identity.Name
			// Context.QueryString

			Groups.Add(Context.ConnectionId, groupId);
		}

		public void ClickCell(string groupName, string cellId)
		{
			Clients.OthersInGroup(groupName).cellClicked(cellId);
			//			Clients.Others.cellClicked(cellId);
		}

		public void TryAction(string boardID, string groupName, string action, string sender)
		{
			int id;
			int.TryParse(boardID, out id);
			Board board = boardRepository.GetBoardById(id);

			board.TryAction(action, sender);			
		}
	}
}
