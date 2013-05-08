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
		private IGameRepo repository = null;

		public HomeController()
		{
			repository = new GameRepo();
		}

		public HomeController(IGameRepo rep)
		{
			repository = rep;
		}
		
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
			

			
            return View();
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

		
    }
}
