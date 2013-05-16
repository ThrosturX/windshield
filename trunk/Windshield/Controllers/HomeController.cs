using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windshield.Common;
using Windshield.Models;
using Windshield.ViewModels;

namespace Windshield.Controllers
{
	public class HomeController : Controller
	{
		private IGameRepo gameRepo = null;
		private IBoardRepo boardRepo = null;
		private IUserRepo userRepo = null;

		public HomeController()
		{
			gameRepo = new GameRepo();
			boardRepo = new BoardRepo();
			userRepo = new UserRepo();
		}

		public HomeController(IGameRepo gRepo, IBoardRepo bRepo, IUserRepo uRepo)
		{
			gameRepo = gRepo;
			boardRepo = bRepo;
			userRepo = uRepo;
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
			var games = gameRepo.GetAllGames();
			return View("Statistics", games);
		}

		public ActionResult DisplayCard()
		{ 
			CardDeck cardDeck = new CardDeck();
			cardDeck.Shuffle();
			return View("DisplayCard", cardDeck);
		}

		public ActionResult GameDescription(Game gameName)
		{
			var viewModel = new GameDescriptionViewModel();
			viewModel.game = gameRepo.GetGameByName(gameName.name);
			if (User.Identity.IsAuthenticated)
			{
				viewModel.gameRating = userRepo.GetGameRatingByGame(userRepo.GetUserByName(User.Identity.Name), viewModel.game);
			}
			return View("GameDescription", viewModel);
		}

		public JsonResult GetStatistics(Game gameName)
		{
			Game game = gameRepo.GetGameByName(gameName.name);

			var statistics = gameRepo.GetTopRatingsForViewModel(game);
				
			return Json(statistics, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetPopularity(Game g)
		{
			
			if (g.name == "All")
			{
				var game = gameRepo.GetTopGamesPlayedForViewModel(null);
				return Json(game, JsonRequestBehavior.AllowGet);
			}
			else if (g.name == "Popular")
			{
				var game = gameRepo.GetTopGamesPlayedForViewModel(5);
				return Json(game, JsonRequestBehavior.AllowGet);
			}

			else // g.name == New
			{
				var game = gameRepo.GetNewGamesPlayedForViewModel();
				return Json(game, JsonRequestBehavior.AllowGet);
			}
		}
	}
}
