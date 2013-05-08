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

			TicTacToe game = new TicTacToe(sally, jonas);

			brd.AddBoard(game);
		}

		[TestMethod]
		public void AddSymbol()
		{
			Assert.AreEqual(1, 1);
			

		}
	}
}
