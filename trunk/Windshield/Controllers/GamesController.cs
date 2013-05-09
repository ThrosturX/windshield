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
		private IUserRepo userRepository = null;

		public GamesController()
		{
			boardRepository = new BoardRepo();
			gameRepository = new GameRepo();
			userRepository = new UserRepo();
		}

		public GamesController(IBoardRepo bRep, IGameRepo gRep, IUserRepo uRep)
		{
			boardRepository = bRep;
			gameRepository = gRep;
			userRepository = uRep;
		}

        public ActionResult Index()
        {
			return View();
		//	return RedirectToAction("Index", "Home");
        }

		[Authorize]
		public ActionResult TicTacToe()
		{
			User playerOne = userRepository.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());
			Board gameBoard = new TicTacToe(playerOne);
			GameInstance theGame = new GameInstance(gameBoard, "TicTacToe");

			gameBoard.startDate = DateTime.Now;
			gameBoard.idOwner = playerOne.UserId;
			gameBoard.status = "";
			gameBoard.Game = gameRepository.GetGameByName("TicTacToe");
			gameBoard.idGame = gameBoard.Game.id;
			boardRepository.AddBoard(gameBoard);

			return View("Index", theGame);
		}
    }
}
