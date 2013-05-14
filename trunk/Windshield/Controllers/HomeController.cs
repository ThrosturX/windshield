﻿using System;
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

			var derp = gameRepo.GetTopRatings(game);

			//ViewData["GameName"] = new SelectList(
			//Games,
		
			return View("Statistics");
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

		public JsonResult GetStatistics(Game gameName)
		{
			Game game = gameRepo.GetGameByName(gameName.name);

			var derp = gameRepo.GetTopRatingsForViewModel(game);

			//TODO: Fix this circular reference thingamajing, however possible
			//might be the only way is to make a viewmodel class which contains exacly the properties
			//we need for our javascript (userName and rating).
			return Json(derp, JsonRequestBehavior.AllowGet);
		}

	}
}
