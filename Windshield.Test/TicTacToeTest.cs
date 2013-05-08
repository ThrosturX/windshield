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
		[TestInitialize]
		public void Setup()
		{
			MockUserRepo rep = new MockUserRepo();

			User sally = new User();
			sally.UserName = "Sally";
			sally.UserId = 1;

			rep.AddUser(sally);
		}

		[TestMethod]
		public void AddSymbol()
		{
			TicTacToe game = new TicTacToe();
		}
	}
}
