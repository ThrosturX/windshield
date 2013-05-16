using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Windshield.Controllers;
using System.Web.Mvc;
using Windshield.Test.MockObjects;
using Windshield.Models;

namespace Windshield.Test.ControllersTest
{
	[TestClass]
	public class GamesControllerTest
	{
		private GamesController games;
	
		private MockBoardRepo boardRepo;
		private MockGameRepo gameRepo;
		private MockUserRepo userRepo;
	
		[TestInitialize]
		public void Setup()
		{
			// Arrange

			boardRepo = new MockBoardRepo();
			gameRepo = new MockGameRepo();
			userRepo = new MockUserRepo();

			List<User> Players = new List<User>();
			User p1 = new User();
			System.Guid a = new Guid();

			p1.UserName = "player1";
			p1.UserId = a;
			userRepo.AddUser(p1);
			Players.Add(p1);			
			
			games = new GamesController(boardRepo, gameRepo, userRepo);

			LiveInstanceRepo liverepo = new LiveInstanceRepo();
		}

/*
		[TestMethod]
		public void CheckTicTacToehasId()
		{
			Board b = new Board();

			

			b.id = (15);
			Player player1 = new Player();
			player1.User = userRepo.GetUserByName("player1");
			player1.idBoard = b.id;
			b.ownerName = "player1";
			boardRepo.AddBoard(b);
			boardRepo.AddPlayer(player1);
			boardRepo.playerTable.Add(player1);

			ViewResult result = games.TicTacToe(b.id) as ViewResult;

			Assert.AreEqual(15, b.id, "BoardIdIsNotEqual");
		}
  */

/*		[TestMethod]
		public void TestGameJoined()
		{
			Board board = new Board();
			board.id = 13;

			GameInstance gameI = new GameInstance(board);


			ViewResult result = games.JoinGame() as ViewResult;
		}*/
	}
}
