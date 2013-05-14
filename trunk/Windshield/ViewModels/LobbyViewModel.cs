using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

namespace Windshield.ViewModels
{
	public class LobbyViewModel
	{
		public int boardId { get; set; }
		public List<User> guests;
	}
}