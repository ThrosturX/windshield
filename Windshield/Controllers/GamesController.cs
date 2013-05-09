using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windshield.Models;
using Windshield.Games.TicTacToe;

namespace Windshield.Controllers
{
    public class GamesController : Controller
    {
        private IBoardRepo boardRepository = null;
		private IGameRepo gameRepository = null;

		public GamesController()
		{
			boardRepository = new BoardRepo();
			gameRepository = new GameRepo();
		}

		public GamesController(IBoardRepo bRep, IGameRepo gRep)
		{
			boardRepository = bRep;
			gameRepository = gRep;
		}

        public ActionResult Index()
        {
			return View();
		//	return RedirectToAction("Index", "Home");
        }

		public ActionResult TicTacToe()
		{
			User mockingbird = new User();
			TicTacToe gameBoard = new TicTacToe(mockingbird);
			GameInstance theGame = new GameInstance(gameBoard, "~/Views/Games/TicTacToe.cshtml");

			return View("Index", theGame);
		}
    }
}
