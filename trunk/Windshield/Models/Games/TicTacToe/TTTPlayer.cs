using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

namespace Windshield.Games.TicTacToe
{
	public class TTTPlayer
	{
			public User user;
			public char symbol;
			public int wins;
			public int losses;
			public int draws;
			public bool isAI;
	}
}