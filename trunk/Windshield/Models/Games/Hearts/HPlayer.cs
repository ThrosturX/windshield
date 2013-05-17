using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

namespace Windshield.Models.Games
{
	public class HPlayer
	{
		public User user;
		public int gamePoints;
		public int matchPoints;
		public Hand hand;

		public HPlayer()
		{
			matchPoints = 0;
			gamePoints = 0;
		}
	}
}