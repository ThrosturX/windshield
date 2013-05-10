using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games
{
	public class TicTacToe : AGame
	{
		static private Coord CellToCoord(int cell)
		{
			Coord result = new Coord();

			int temp = cell;

			result.x = cell % 3;
			result.y = cell / 3;

			return result;
		}

		public char[,] grid;
		public int freeSquares;
		private TTTPlayer turn;
		private Board board;

		public TTTPlayer player1 { get; set; }	// The owner is Player One
		public TTTPlayer player2 { get; set; }

		public TicTacToe ()
		{
			Board board = new Board();
			board.status = "";
			grid = new char[3, 3];
			ClearBoard();
			InitializePlayers();
		}

		public TicTacToe(List<User> players) : this()
		{
			// TODO try catch á player1.user
			player1.user = players[0];
			if (players.Count > 1)
			{
				player2.user = players[1];
			}
			else
			{
				player2.user.UserName = "Computer";
				player2.isAI = true;
			}
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
			player1 = new TTTPlayer();
			player2 = new TTTPlayer();

			player1.symbol = 'X';
			player2.symbol = 'O';

			player1.wins = 0;
			player1.losses = 0;
			player1.draws = 0;

			player2.wins = 0;
			player2.losses = 0;
			player2.draws = 0;
		}

		/// <summary>
		/// Inserts a symbol into a cell in the game grid.
		/// </summary>
		/// <param name="symbol"></param>
		/// <param name="cell"></param>
		/// <returns></returns>
		public bool InsertSymbol(char symbol, int cell)
		{
			Coord location = CellToCoord(cell);
			if (cell < 0 || cell > 8)
			{
				return false;
			}

			if (grid[location.x, location.y] != ' ')
			{
				return false;
			}

			grid[location.x, location.y] = symbol;
			if (--freeSquares < 0)
			{
				throw new Exception("InsertSymbol: Board seems erroneously full!");
			}

			// swap players
			SwitchTurns();

			return true;
		}

		/// <summary>
		/// Switches whose turn it is
		/// </summary>
		private void SwitchTurns()
		{
			if (turn == player1)
			{
				turn = player2;
			}
			else
			{
				turn = player1;
			}
		}

		private string Status()
		{
			return "lol";
		}

		/// <summary>
		/// Converts a Hub request into business logic. Currently implements:
		/// * insert: attempts to place a symbol on the grid
		/// </summary>
		/// <param name="action">string: "insert cell#" where # is a number between 0 and 8</param>
		/// <param name="sender">The username of the player attempting the action.</param>
		/// <returns></returns>
		public override bool TryAction(string action, string sender)
		{
			// check who sent it
			TTTPlayer player;

			if (player1.user.UserName == sender)
			{
				player = player1;
			}
			else if (player2.user.UserName == sender)
			{
				player = player2;
			}
			else
			{
				player = null;
			}

			// check what action was tried

			if (action.Contains("insert"))
			{
				// make sure a valid player is trying to insert
				if (player == null)
				{
					return false;
				}

				// check if it is player's turn
				if (turn != player)
					return false;

				// find out where it should be inserted
				if (action.Contains("cell"))
				{
					// find which cell it is
					int index = action.IndexOf("cell");
					int cell;

					string cellString = action.Substring(index + 4, 1);

					// ensure that the string is not malformed
					if (!int.TryParse(cellString, out cell))
						return false;

					// attempt to insert the symbol
					return InsertSymbol(player.symbol, cell);
				}
			}

			// No success
			return false;
		}

		public class TTTPlayer
		{
			public User user;
			public char symbol;
			public int wins;
			public int losses;
			public int draws;
			public bool isAI;
		}

		private struct Coord
		{
			public int x;
			public int y;
		}


	}

}