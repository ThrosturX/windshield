using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games
{
	public class TicTacToe : AGame
	{
		public char[,] grid;
		public int freeSquares;

		public TTTPlayer playerOne { get; set; }	// The owner is Player One
		public TTTPlayer playerTwo { get; set; }

		public TicTacToe()
		{
			grid = new char[3, 3];
			ClearBoard();
			InitializePlayers();
		}

		/// <summary>
		/// Clears the board from all symbols.
		/// </summary>
		public void ClearBoard()
		{
			for (int i = 0; i < 3; ++i)
			{
				for (int j = 0; j < 3; ++j)
				{
					grid[i, j] = ' ';
				}
			}

			freeSquares = 9;
		}

		/// <summary>
		/// Gives the game's players initial values for symbols, wins, losses and draws.
		/// </summary>
		public void InitializePlayers()
		{
			playerOne = new TTTPlayer();
			playerTwo = new TTTPlayer();

			playerOne.symbol = 'X';
			playerTwo.symbol = 'O';

/*			playerOne.wins = 0;
			playerOne.losses = 0;
			playerOne.draws = 0;

			playerTwo.wins = 0;
			playerTwo.losses = 0;
			playerTwo.draws = 0;*/
		}



		public override bool TryMove(Board board, string move)
		{
			return true;
		}

		private string Status()
		{
			return "lol";
		}
		public class TTTPlayer
		{
			public User user;
			public char symbol;
//			public int wins;
//			public int losses;
//			public int draws;
			public bool isAI;
		}

	}

}