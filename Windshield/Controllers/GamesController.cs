using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windshield.Models;
using Windshield.Models.Games;

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
			List<User> players = new List<User>();
			User playerOne = userRepository.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());
			players.Add(playerOne);
			User playerTwo = userRepository.GetUserByName("banana");
			players.Add(playerTwo);
			TicTacToe gameBoard = new TicTacToe(players);
			GameInstance theGame = new GameInstance(gameBoard.board.id, gameBoard, "TicTacToe");

			gameBoard.board.idOwner = playerOne.UserId;
			gameBoard.board.Game = gameRepository.GetGameByName("TicTacToe");
			gameBoard.board.idGame = gameBoard.board.Game.id;
			boardRepository.AddBoard(gameBoard.board);

			return View("Index", theGame);
		}
    }
}
