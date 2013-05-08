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
		private  IGameRepo repository = new GameRepo();

		public HomeController()
		{
			//repository = new GameRepo();
		}

		public HomeController(IGameRepo rep)
		{
			repository = rep;
		}
		
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
			

			
            return View("Index", repository);
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

			
		public ActionResult Stats()
		{
			ViewBag.Message = "Your contact page.";

			return View("Stats");
		}

		public ActionResult MyGames()
		{
			ViewBag.Message = "Your contact page.";

			return View("MyGames");
		}

		public ActionResult GameDescription(Game game)
		{
			return View("GameDescription", game);
		}

		public ActionResult GameLobby(Game game)
		{
			return View("GameLobby", game);
		}

		public ActionResult derp()
		{
			if (User.Identity.IsAuthenticated)
			{
				return View("Stats");
			}
			else
			{
				return View("About");
			}
		}
    }
}
