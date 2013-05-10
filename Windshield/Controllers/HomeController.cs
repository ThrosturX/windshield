using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windshield.Models;

namespace Windshield.Controllers
{
    public class HomeController : Controller
    {
		private IGameRepo gameRepo = new GameRepo();
		private IBoardRepo boardRepo = new BoardRepo();
		
		public HomeController()
		{
			//repository = new GameRepo();
		}

		public HomeController(IGameRepo repo)
		{
			gameRepo = repo;
		}
		
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
			

			
            return View("Index", gameRepo);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

			
		public ActionResult Statistics()
		{
			ViewBag.Message = "Your contact page.";

			return View("Statistics");
		}

		public ActionResult MyGames()
		{
			ViewBag.Message = "Your contact page.";

			return View("MyGames");
		}

		public ActionResult GameDescription(Game gameName)
		{
			var game = gameRepo.GetGameByName(gameName.name);
			return View("GameDescription", game);
		}

		public ActionResult GameLobby(Game game)
		{
			return View("GameLobby", game);
		}

		public ActionResult DisplayCard()
		{
			return View("DisplayCard");
		}

		public ActionResult Boards(Game game)
		{
			

			if (game.name == "game")
			{
				var viewModel = boardRepo.GetBoards(gameRepo.GetGameByName("game"));
				return View("Boards", viewModel.ToList());
			}
			else if (game.name == "TicTacToe")
			{
				var viewModel = boardRepo.GetBoards(gameRepo.GetGameByName("TicTacToe"));
				return View("Boards", viewModel.ToList());
			}
			else
			{
				var ViewModel = boardRepo.GetBoards(gameRepo.GetGameByName("game"));
				return View("Boards", ViewModel.ToList());
			}
		}
    }
}
