using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windshield.Common;
using Windshield.Models;

namespace Windshield.Controllers
{
    public class HomeController : Controller
    {
		private IGameRepo gameRepo = new GameRepo();
		private IBoardRepo boardRepo = new BoardRepo();
		private IUserRepo userRepo = new UserRepo();
		
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
			Game game = gameRepo.GetGameByName("Tic Tac Toe");

			var derp = userRepo.GetTopUsersByGame(game);
			return View("Statistics", derp);
		} 

		public ActionResult DisplayCard()
		{
			CardDeck cardDeck = new CardDeck();
			cardDeck.Shuffle();
			return View("DisplayCard", cardDeck);
		}

		public ActionResult GameDescription(Game gameName)
		{
			var game = gameRepo.GetGameByName(gameName.name);
			return View("GameDescription", game);
		}

    }
}
