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
		private MockUserRepo usr;
		private MockBoardRepo brd;
		private MockGameRepo gam;
	
		[TestInitialize]
		public void Setup()
		{
			// Arrange
			usr = new MockUserRepo();
			brd = new MockBoardRepo();
			gam = new MockGameRepo();
			
			Game g1 = new Game();
			Game g2 = new Game();
			Game g3 = new Game();
			Game g4 = new Game();
			Game g5 = new Game();
			Game g6 = new Game();

			g1.name = "g1";
			g1.id = 1;

			gam.AddGame(g1);
			gam.AddGame(g2);
			gam.AddGame(g3);
			gam.AddGame(g4);
			gam.AddGame(g5);
			gam.AddGame(g6);

			

		
			home = new HomeController(gam, brd, usr);
		}

		[TestMethod]
		public void CountGettingAllForIndex()
		{

			ViewResult Result = home.Index() as ViewResult;

			var games = Result.Model as IEnumerable<Game>;

			Assert.AreEqual(6, games.Count(), "GameCountisNotEqual");
		}

		[TestMethod]
		public void CheckGameDescription()
		{
			Game theGame = new Game();
			theGame.name = "TheGame";
			theGame.id = 2;
			theGame.maxPlayers = 4;
			theGame.timesPlayed = 9;
			theGame.description = "asdf";

			gam.AddGame(theGame);

			ViewResult Result = home.GameDescription(theGame) as ViewResult;

			var gamename = Result.Model as Game;
	
			Assert.AreEqual("TheGame",gamename.name , "GameNameisNotEqual");
		
		}

	}
}
