using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games
{
	public class TPlayer
	{
		public User user;
		public char symbol;
		public int wins;
		public int losses;
		public int draws;

		internal bool isAI()
		{
			if (user == null)
			{
				return true;
			}
			return (user.UserName.StartsWith("Computer"));
		}
	}
}