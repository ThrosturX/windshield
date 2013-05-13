using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Windshield.Test")]

namespace Windshield.Models.Games.TicTacToe
{
	/// <summary>
	/// A Tic Tac Toe logic and view model class
	/// </summary>
	/// <remarks>
	/// When creating a new board
	///   List<User> players = ...;  // some list of one to two users
	///   var ttt = new TicTacToe(players);
	///   Board board = new Board();
	///   board.status = ttt.GetStatus();
	///   board.idGame = ...;  // get id of the game from the repo
	///   boardRepo.AddBoard(board);
	///   boardRepo.Save()
	/// When getting the board from database
	///   Board board = ...;  // get board from repo
	///   var ttt = new TicTacToe();
	///   ttt.SetStatus(board.status);
	///   if (ttt.TryAction(...))
	///   {
	///	      board.status = ttt.GetStatus();
	///	      boardRepo.Save();
	///	      // broadcast with SignalR
	///	  }
	///   
	/// </remarks>
	public class TicTacToe : AGame
	{
		/// <summary>
		/// Converts a cell number to a coordinate struct.
		/// </summary>
		static internal Coord CellToCoord(int cell)
		{
			Coord result = new Coord();

			int temp = cell;

			result.x = cell % 3;
			result.y = cell / 3;

			return result;
		}

		/// <summary>
		/// Converts a coordinate struct to a cell number.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		static internal int CoordToCell(Coord input)
		{
			return (input.y * 3 + input.x);
		}

		public char[,] grid;
		internal int freeSquares;
		internal TPlayer turn;

		public TPlayer player1 { get; set; }	// The owner is Player One
		public TPlayer player2 { get; set; }

		public TicTacToe()
		{
			grid = new char[3, 3];
			ClearBoard();
			InitializePlayers();
		}

		public TicTacToe(List<User> players)
			: this()
		{
			// TODO try catch á player1.user
			player1.user = players[0];
			if (players.Count > 1)
			{
				player2.user = players[1];
			}
			else
			{
				player2.user = new User();
				player2.user.UserName = "Computer";
				player2.isAI = true;
			}
			//TODO: Þarf þetta nokkuð ?   board.idOwner = player1.user.UserId;
		}

		/// <summary>
		/// Clears the board from all symbols.
		/// </summary>
		internal void ClearBoard()
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
		internal void InitializePlayers()
		{
			player1 = new TPlayer();
			player2 = new TPlayer();

			player1.symbol = 'X';
			player2.symbol = 'O';

			player1.wins = 0;
			player1.losses = 0;
			player1.draws = 0;

			player2.wins = 0;
			player2.losses = 0;
			player2.draws = 0;

			turn = player1;
		}

		/// <summary>
		/// Inserts a symbol into a cell in the game grid.
		/// </summary>
		internal bool InsertSymbol(char symbol, int cell)
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
		internal void SwitchTurns()
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

		/// <summary>
		/// Returns the Tic Tac Toe Player that uses the specified symbol (X or O).
		/// </summary>
		/// <param name="symbol"></param>
		/// <returns></returns>
		internal TPlayer GetPlayerBySymbol(char symbol)
		{
			if (player1.symbol == symbol)
			{
				return player1;
			}
			if (player2.symbol == symbol)
			{
				return player2;
			}

			throw new ArgumentException("No player exists with this symbol"); // this should never happen
		}

		/// <summary>
		/// Checks if there is a winner.
		/// </summary>
		/// <returns>Tic Tac Toe Player that has won the game.</returns>
		internal TPlayer CheckWinner()
		{
			char center;
			int i;
			// check rows
			for (i = 0; i < 3; ++i)
			{
				if (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i])
				{
					// check for player symbol
					if (grid[0, i] != ' ')
					{
						return GetPlayerBySymbol(grid[0, i]);
					}
				}
			}

			// check columns
			for (i = 0; i < 3; ++i)
			{
				if (grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2])
				{
					// check for player symbol
					if (grid[i, 0] != ' ')
					{
						return GetPlayerBySymbol(grid[i, 0]);
					}
				}
			}

			// check diagonals
			center = grid[1, 1];
			if ((center == grid[0, 0] && center == grid[2, 2]) || (center == grid[0, 2] && center == grid[2, 0]))
			{
				// check for player symbol
				if (center != ' ')
				{
					return GetPlayerBySymbol(center);
				}
			}

			// there is no winner
			return null;
		}

		/// <summary>
		/// swaps the player's symbols if they should elect to play a new game and adjusts their scores
		/// </summary>
		/// <param name="winner">should be null if there is a draw</param>
		internal void EndGame(TPlayer winner)
		{
			// swap symbols
			char temp = player1.symbol;
			player1.symbol = player2.symbol;
			player2.symbol = temp;

			// check if this is a drawing situation
			if (winner == null)
			{
				player1.draws++;
				player2.draws++;
			}
			else
			{
				TPlayer loser;
				// infer winner from loser
				if (player1 == winner)
				{
					loser = player2;
				}
				else
				{
					loser = player1;
				}

				// adjust temporary scores
				winner.wins++;
				loser.losses++;
			}
		}

		/// <summary>
		/// Selects a cell to be picked by the AI.
		/// </summary>
		/// <returns>A cell number to insert the symbol.</returns>
		internal int AISelectCell()
		{
			Coord selected = new Coord();
			Random rnd = new Random();
			// try and get the center
			if (grid[1, 1] == ' ')
			{
				selected.x = 1;
				selected.y = 1;
				return CoordToCell(selected);
			}

			// select a random cell until it finds a free cell
			do
			{
				selected.x = rnd.Next(3);
				selected.y = rnd.Next(3);
				if (grid[selected.x, selected.y] == ' ')
				{
					return CoordToCell(selected);
				}
			}
			while (true);
		}

		/// <summary>
		/// Execute any necessary actions when an action has been completed by a client
		/// </summary>
		internal int ActionCompleted()
		{
			TPlayer winner;
			if (freeSquares == 0)
			{
				winner = CheckWinner();
				return 2;
			}

			if (player2.isAI)
			{
				InsertSymbol(player2.symbol, AISelectCell());
			}

			winner = CheckWinner();
			if (winner != null)
			{
				return 2;
			}

			return 1;
		}

		/// <summary>
		/// Status() converts the game's state into a string format, for storage in databases.
		/// The outputted string can be used in a constructor.
		/// </summary>
		/// <remarks>
		/// Format: [grid]|[the player whose turn it is(1 or 2)]|[player1's symbol]|[p1's wins]|[p1's draws]|[p1's losses]|[User1 name]|[User2 name]
		///			grid is 9 characters and indicates the cell's values ('X', 'O' or ' ')
		///			turn# indicates which player's turn it is.
		///			S indicates player1's symbol.
		///			Wins, Losses and Ties indicate player 1's WLT's.
		///			Example:
		///			XO OXXO X|1|X|3|2|1|john|banana
		/// </remarks>
		/// <returns>A database-friendly string that can be converted into the game's state.</returns>
		internal string GetStatus()
		{
			StringBuilder builder = new StringBuilder();
			//grid
			for (int i = 0; i < 9; i++)
			{
				builder.Append(grid[CellToCoord(i).x, CellToCoord(i).y]);
			}
			// separator 1
			builder.Append("|");
			//turn
			if (turn == player1)
			{
				builder.Append("1");
			}
			else
			{
				builder.Append("2");
			}
			// Symbol
			builder.Append("|" + turn.symbol + "|");
			// Wins
			builder.Append(turn.wins + "|");
			// Draws
			builder.Append(turn.draws + "|");
			// Losses
			builder.Append(turn.losses + "|");

			// players
			builder.Append(player1.user.UserName + "|" + player2.user.UserName);

			return builder.ToString();
		}

		internal string GetGameOver()
		{
			TPlayer winner = CheckWinner();
			EndGame(winner);
			ClearBoard();
			if (winner != null)
				return winner.user.UserName;

			return "";
		}

		/// <summary>
		/// Initiates the board
		/// </summary>
		internal void SetStatus(string status)
		{
			string[] strings = status.Split('|');

			// Process grid
			for (int i = 0; i < 9; ++i)
			{
				// find coordinate
				Coord loc;

				loc = CellToCoord(i);

				grid[loc.x, loc.y] = strings[0][i];

				if (strings[0][i] != ' ')
				{
					--freeSquares;
				}
			}

			// process p1 stats

			// turn
			if (strings[1][0] == '1')
			{
				turn = player1;
			}
			else
			{
				turn = player2;
			}

			// symbol
			player1.symbol = strings[2][0];

			// wins
			int.TryParse(strings[3], out player1.wins);

			// draws
			int.TryParse(strings[4], out player1.draws);

			// losses
			int.TryParse(strings[5], out player1.losses);

			// player 2
			if (player1.symbol == 'X')
			{
				player2.symbol = 'O';
			}
			else
			{
				player2.symbol = 'X';
			}

			player2.wins = player1.losses;
			player2.losses = player2.wins;
			player2.draws = player1.draws;

		}

		/// <summary>
		/// Converts a Hub request into business logic. Currently implements:
		/// * insert: attempts to place a symbol on the grid
		/// </summary>
		/// <param name="action">string: "insert cell#" where # is a number between 0 and 8</param>
		/// <param name="sender">The username of the player attempting the action.</param>
		/// <returns>returns 0 for failure, 1 for OK and 2 for Game Over</returns>
		public override int TryAction(string action, string sender)
		{
			// check who sent it
			TPlayer player;

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
					return 0;
				}

				// check if it is player's turn
				if (turn != player)
					return 0;

				// find out where it should be inserted
				if (action.Contains("cell"))
				{
					// find which cell it is
					int index = action.IndexOf("cell");
					int cell;

					string cellString = action.Substring(index + 4, 1);

					// ensure that the string is not malformed
					if (!int.TryParse(cellString, out cell))
						return 0;

					// attempt to insert the symbol
					if (InsertSymbol(player.symbol, cell))
					{
						return ActionCompleted();
					};
				}
			}

			// No success
			return 0;
		}

		internal struct Coord
		{
			public int x;
			public int y;
		}


	}

}