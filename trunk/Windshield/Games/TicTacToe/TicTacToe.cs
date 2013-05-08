using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

namespace Windshield.Games.TicTacToe
{
	public struct Coord
	{
		public int x;
		public int y;
	}

	public class TicTacToe : Board
	{
		public static string name = "Tic Tac Toe";
		public static string description =    "Tic Tac Toe is a two player game. The goal of Tic Tac Toe is to be the first player "
											+ "to get three symbols in a row on a 3x3 grid. Each player gets a symbol, either X or O. "
											+ "The player with X starts. Players alternate placing Xs and Os on the board until either"
											+ " (a) one player has three in a row, horizontally, vertically or diagonally; or (b) all "
											+ "nine squares are filled. \nIf a player is able to draw three Xs or three Os in a row, "
											+ "that player wins. \nIf all nine squares are filled and neither player has three in a row"
											+ ", the game is a draw.";

		public static List<string> rules =  new List<string> {
										  "The player with X as a token starts the game"
										, "Players alternate placing Xs and Os on the board"
										, "If a player is able to draw three Xs or three Os in a row, that player wins"
										, "If all nine squares are filled and neither player has three in a row, the game is a draw"
									};

		public char[,] grid;
		public int freeSquares;

		public TTTPlayer playerOne { get; set; }	// The owner is Player One
		public TTTPlayer playerTwo { get; set; }

		/// <summary>
		/// Default constructor. Note, instances require players to be instantiated!
		/// </summary>
		public TicTacToe()
		{
			grid = new char[3, 3];
			ClearBoard();
			InitializePlayers();
		}

		/// <summary>
		/// Players VS Computer constructor.
		/// </summary>
		/// <param name="_playerOne"></param>
		public TicTacToe(User _playerOne) : this()
		{
			playerOne.user = _playerOne;
			playerTwo.user = new User();
			playerTwo.user.UserName = "Computer";
			playerTwo.isAI = true;
		}

		/// <summary>
		/// Two player constructor. Human vs Human.
		/// </summary>
		/// <param name="_playerOne"></param>
		/// <param name="_playerTwo"></param>
		public TicTacToe(User _playerOne, User _playerTwo) : this() 
		{
			playerOne.user = _playerOne;
			playerTwo.user = _playerTwo;
		}

		/// <summary>
		/// Converts a cell number to a coordinate struct.
		/// </summary>
		/// <param name="cell"></param>
		/// <returns></returns>
		static public Coord CellToCoord(int cell)
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
		static public int CoordToCell(Coord input)
		{
			return (input.y * 3 + input.x);
		}

		/// <summary>
		/// This function should convert the game's "state" to a string for DB storage. This has not been implemented.
		/// </summary>
		/// <param name="saveString"></param>
		public TicTacToe(string saveString)
		{
			throw new NotImplementedException("SaveStrings have not been implemented.");
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
			if (grid[location.x, location.y] != ' ')
				return false;

			grid[location.x, location.y] = symbol;
			if (--freeSquares < 0)
			{
				throw new Exception("InsertSymbol: Board seems erroneously full!");
			}
			return true;
		}

		/// <summary>
		/// Returns the Tic Tac Toe Player that uses the specified symbol (X or O).
		/// </summary>
		/// <param name="symbol"></param>
		/// <returns></returns>
		public TTTPlayer GetPlayerBySymbol(char symbol)
		{
			if (playerOne.symbol == symbol)
			{
				return playerOne;
			}
			if (playerTwo.symbol == symbol)
			{
				return playerTwo;
			}

			throw new ArgumentException("No player exists with this symbol"); // this should never happen
		}

		/// <summary>
		/// Checks if there is a winner.
		/// </summary>
		/// <returns>Tic Tac Toe Player that has won the game.</returns>
		public TTTPlayer CheckWinner()
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

			playerOne.wins   =  0 ;
			playerOne.losses =  0 ;
			playerOne.draws  =  0 ;
			
			playerTwo.wins   =  0 ;
			playerTwo.losses =  0 ;
			playerTwo.draws  =  0 ;
		}

		/// <summary>
		/// swaps the player's symbols if they should elect to play a new game and adjusts their scores
		/// </summary>
		/// <param name="winner">should be null if there is a draw</param>
		public void EndGame(TTTPlayer winner)
		{
			// swap symbols
			char temp = playerOne.symbol;
			playerOne.symbol = playerTwo.symbol;
			playerTwo.symbol = temp;

			// check if this is a drawing situation
			if (winner == null)
			{
				playerOne.draws++;
				playerTwo.draws++;
			}
			else
			{
				TTTPlayer loser;
				// infer winner from loser
				if (playerOne == winner)
				{
					loser = playerTwo;
				}
				else
				{
					loser = playerOne;
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
		public int AISelectCell()
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
		/// ToSaveString() converts the game's state into a string format, for storage in databases.
		/// The outputted string can be used in a constructor.
		/// </summary>
		/// <returns>A database-friendly string that can be converted into the game's state.</returns>
		public string ToSaveString()
		{
			throw new NotImplementedException("ToSaveString is not supported");
		}
	}
}