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
	public class HomeControllerTest
	{
		private HomeController home;
		private MockUserRepo userRepo;
		private MockBoardRepo boardRepo;
		private MockGameRepo gameRepo;
	
		[TestInitialize]
		public void Setup()
		{
			userRepo = new MockUserRepo();
			boardRepo = new MockBoardRepo();
			gameRepo = new MockGameRepo();
			
			Game g1 = new Game();
			Game g2 = new Game();
			Game g3 = new Game();
			Game g4 = new Game();
			Game g5 = new Game();
			Game g6 = new Game();

			g1.name = "g1";
			g1.id = 1;

			gameRepo.AddGame(g1);
			gameRepo.AddGame(g2);
			gameRepo.AddGame(g3);
			gameRepo.AddGame(g4);
			gameRepo.AddGame(g5);
			gameRepo.AddGame(g6);

			home = new HomeController(gameRepo, boardRepo, userRepo);
		}

		[TestMethod]
		public void CountGettingAllForIndex()
		{
			ViewResult Result = home.Index() as ViewResult;

			var games = Result.Model as IEnumerable<Game>;

			Assert.AreEqual(6, games.Count(), "GameCountisNotEqual");
		}
	}
}
