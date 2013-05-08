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
			game.InsertSymbol('T', 0);

			Assert.AreEqual('T', game.grid[0, 0]);

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
	}
}
