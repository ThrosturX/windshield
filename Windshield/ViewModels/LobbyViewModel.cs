using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

namespace Windshield.ViewModels
{
	//Getting Information for the lobby view.
	public class LobbyViewModel
	{
		public int boardId { get; set; }
		public List<User> guests;
		public string Image { get; set; }
		public string theName { get; set; }
		public string modelName { get; set; }
		public string ownerName { get; set; }
	}
}