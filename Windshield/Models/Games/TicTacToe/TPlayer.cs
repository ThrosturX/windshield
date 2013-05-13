using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games.TicTacToe
{
	public class TPlayer
	{
		public User user;
		public char symbol;
		public int wins;
		public int losses;
		public int draws;
		public bool isAI;
	}
}