using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;


namespace Windshield.Controllers
{
//	[Authorize]
	public class GameHub : Hub
	{
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
	}
}
