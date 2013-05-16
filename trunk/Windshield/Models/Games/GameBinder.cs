using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games
{
	public class GameBinder
	{
		public static IGame GameObject(string gameName, Board board)
		{
			switch(gameName)
			{
				case "TicTacToe":
					// TODO: 
					// return new TicTacToe(board);
					break;
			}
			return null;
		}
	}
}