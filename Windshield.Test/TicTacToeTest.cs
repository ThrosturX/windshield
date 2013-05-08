using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windshield.Games.TicTacToe;
using Windshield.Models;
using Windshield.Test.MockObjects;

namespace Windshield.Test
{
	[TestClass]
	public class TicTacToeTest
	{
		private MockUserRepo usr;
		private MockBoardRepo brd;
		private TicTacToe game;

		[TestInitialize]
		public void Setup()
		{
			usr = new MockUserRepo();
			brd = new MockBoardRepo();

			User sally = new User();
			User jonas = new User();
			sally.UserName = "Sally";
			jonas.UserName = "Jonas";
			usr.AddUser(sally);
			usr.AddUser(jonas);

			game = new TicTacToe(sally, jonas);

			brd.AddBoard(game);
		}

		[TestMethod]
		public void InsertSymbol()
		{
			game.InsertSymbol('1', 1);
			game.InsertSymbol('2', 2);
			game.InsertSymbol('3', 3);
			game.InsertSymbol('4', 4);
			game.InsertSymbol('5', 5);
			game.InsertSymbol('6', 6);
			game.InsertSymbol('7', 7);
			game.InsertSymbol('8', 8);
			game.InsertSymbol('0', 0);

			Assert.AreEqual('0', game.grid[0, 0]);
			Assert.AreEqual('1', game.grid[1, 0]);
			Assert.AreEqual('2', game.grid[2, 0]);
			Assert.AreEqual('3', game.grid[0, 1]);
			Assert.AreEqual('4', game.grid[1, 1]);
			Assert.AreEqual('5', game.grid[2, 1]);
			Assert.AreEqual('6', game.grid[0, 2]);
			Assert.AreEqual('7', game.grid[1, 2]);
			Assert.AreEqual('8', game.grid[2, 2]);
		}

		[TestMethod]
		public void CoordToCell()
		{
			Coord middleLeft;
			middleLeft.x = 0;
			middleLeft.y = 1;

			Coord first;
			first.x = 0;
			first.y = 0;

			Coord last;
			last.x = 2;
			last.y = 2;

			Assert.AreEqual(3, TicTacToe.CoordToCell(middleLeft));
			Assert.AreEqual(0, TicTacToe.CoordToCell(first));
			Assert.AreEqual(8, TicTacToe.CoordToCell(last));

		}

		
		[TestMethod]
		public void CellToCoord()
		{
			Coord middleGot;
			Coord middleExpected;

			middleGot = TicTacToe.CellToCoord(4);
			middleExpected.x = 1;
			middleExpected.y = 1;

			Coord expectedCoord;
			Coord gotCoord;

			Assert.AreEqual(middleExpected.x, middleGot.x);
			Assert.AreEqual(middleExpected.y, middleGot.y);

			// Idemnity tests
			expectedCoord.x = 0;
			expectedCoord.y = 1;
			gotCoord = TicTacToe.CellToCoord(TicTacToe.CoordToCell(expectedCoord));
			Assert.AreEqual(expectedCoord, gotCoord);

			expectedCoord.x = 2;
			expectedCoord.y = 0;
			gotCoord = TicTacToe.CellToCoord(TicTacToe.CoordToCell(expectedCoord));
			Assert.AreEqual(expectedCoord, gotCoord);

			expectedCoord.x = 2;
			expectedCoord.y = 1;
			gotCoord = TicTacToe.CellToCoord(TicTacToe.CoordToCell(expectedCoord));
			Assert.AreEqual(expectedCoord, gotCoord);
		}


		//Test methods to test ClearBoard()
		[TestMethod]
		public void ClearBoard1()
		{
			game.ClearBoard();
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					Assert.AreEqual(game.grid[i, j], ' ');
				}
			}
		}
		[TestMethod]
		public void ClearBoard2()
		{
			// fill the board
			for (int i = 0; i < 9; i++)
			{
				game.InsertSymbol('T', 0);
			}
			// empty the board
			game.ClearBoard();
			// assert that the board is empty
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					Assert.AreEqual(game.grid[i, j], ' ');
				}
			}
		}

		//Test method to test GetPlayerBySymbol
		[TestMethod]
		public void GetPlayerBySymbol()
		{
			Assert.AreEqual(game.GetPlayerBySymbol('X'), game.playerOne);
			Assert.AreEqual(game.GetPlayerBySymbol('O'), game.playerTwo);
		}

		[TestMethod]
		public void InitializePlayers()
		{

		}

		[TestMethod]
		public void EndGame()
		{

		}

		[TestMethod]
		public void AISelectCell()
		{

		}

	}
}
