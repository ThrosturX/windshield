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
        private IBoardRepo repository = null;


		public GamesController()
		{
				repository = new BoardRepo();
		}

		public GamesController(IBoardRepo rep)
		{
			repository = rep;
		}

        public ActionResult Index()
        {
			return View();
		//	return RedirectToAction("Index", "Home");
        }

		public ActionResult Dummy()
		{
			return View("Index");
		}

		public ActionResult TicTacToe()
		{
			
			return View("Index");
		}
		
    }
}
