using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Games.TicTacToe;

namespace Windshield.Models
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

		public TicTacToe()
		{
			grid = new char[3, 3];
			ClearBoard();
			InitializePlayers();
		}

		public TicTacToe(User _playerOne, User _playerTwo) : this() 
		{
			playerOne.user = _playerOne;
			playerTwo.user = _playerTwo;
		}

		public TicTacToe(string saveString)
		{
			throw new NotImplementedException("SaveStrings have not been implemented.");
		}

		public Coord CellToCoord(int cell)
		{
			Coord result = new Coord();

			int temp = cell;

			result.y = cell % 3;
			result.x = 0;

			while (temp > 0)
			{
				result.x++;
				temp /= 3;
			}

			return result; 
		}

		public int CoordToCell(Coord input)
		{
			return (input.y * 3 + input.x);
		}

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

		public TTTPlayer CheckWinner()
		{
			char center;
			int i;
			// check rows
			for (i = 0; i < 3; ++i)
			{
				if (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i])
				{
					return GetPlayerBySymbol(grid[0, i]);
				}
			}

			// check columns
			for (i = 0; i < 3; ++i)
			{
				if (grid[i, 0] == grid[1, i] && grid[1, i] == grid[2, i])
				{
					return GetPlayerBySymbol(grid[i, 0]);
				}
			}

			// check diagonals
			center = grid[1, 1];
			if ((center == grid[0, 0] && center == grid[2, 2]) || center == grid[0, 2] && center == grid[2, 0])
			{
				return GetPlayerBySymbol(center);
			}

			// there is no winner
			return null;
		}

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

		public void InitializePlayers()
		{
			playerOne.symbol = 'X';
			playerTwo.symbol = 'O';

			playerOne.wins   =  0 ;
			playerOne.losses =  0 ;
			playerOne.draws  =  0 ;
			
			playerTwo.wins   =  0 ;
			playerTwo.losses =  0 ;
			playerTwo.draws  =  0 ;
		}

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
				loser.losses--;
			}
		}

		int AISelectCell()
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