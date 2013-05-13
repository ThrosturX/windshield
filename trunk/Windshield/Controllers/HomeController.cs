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
            return View("Index", gameRepo.GetAllGames());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

			
		public ActionResult Statistics()
		{
			return View("Statistics");
		}

		public ActionResult MyGames()
		{
			return View("MyGames");
		}

		public ActionResult GameDescription(Game gameName)
		{
			var game = gameRepo.GetGameByName(gameName.name);
			return View("GameDescription", game);
		}

		

		public ActionResult DisplayCard()
		{
			return View("DisplayCard");
		}

		public ActionResult Boards(Game game)
		{
			var name = gameRepo.GetGameByName(game.name);
			var viewModel = boardRepo.GetBoards(name);
			return View("Boards", viewModel);
		}
    }
}
