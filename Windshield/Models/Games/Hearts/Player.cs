using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

namespace Windshield.Games.Hearts
{
	public class Player
	{
		public User user;
		public int gamePoints;
		public int matchPoints;
		public Hand hand;

		public Player()
		{
			matchPoints = 0;
			gamePoints = 0;
		}
	}
}