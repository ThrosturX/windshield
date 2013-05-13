using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Windshield.Models;
using Windshield.Models.Games;

namespace Windshield.Controllers
{
    public class GamesController : Controller
    {
        private BoardRepo boardRepository = null;
		private IGameRepo gameRepository = null;
		private IUserRepo userRepository = null;

		public GamesController()
		{
			boardRepository = new BoardRepo();
			gameRepository = new GameRepo();
			userRepository = new UserRepo();
		}

		public GamesController(BoardRepo bRep, IGameRepo gRep, IUserRepo uRep)
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

			boardRepository.AddBoard(gameBoard.board);
			
			gameBoard.board.idOwner = playerOne.UserId;
			gameBoard.board.idGame = gameRepository.GetGameByName("Tic Tac Toe").id;
			boardRepository.Save();

			return View("Index", theGame);
		}

		[Authorize]
		public ActionResult NewBoard(string gameName)
		{
			List<User> players = new List<User>();
			User playerOne = userRepository.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());
			players.Add(playerOne);
			User playerTwo = userRepository.GetUserByName("banana"); //mock user
			players.Add(playerTwo);                                  //mock user

			Game game = gameRepository.GetGameByName(gameName);
			if (game == null)
			{
				return View("Error");
			}
			else
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				Type gameType = executingAssembly.GetType("Windshield.Models.Games." + game.model);

				//object gameInstance = Activator.CreateInstance(gameType); <-- óþarfi
				MethodInfo getFullNameMethod = gameType.GetMethod("GetFullName");


				ConstructorInfo ctor = gameType.GetConstructor(new[] { typeof(List<User>) });
				object gameInstance = ctor.Invoke(new object[] { players });
				
				return View("GameLobby", gameInstance);
			}
		}

		public ActionResult GameLobby(Game game)
		{
			return View("GameLobby", game);
		}

		public ActionResult Boards(Game game)
		{
			var name = gameRepository.GetGameByName(game.name);
			var viewModel = boardRepository.GetBoards(name);
			return View("Boards", viewModel);
		}

		public ActionResult GameDescription(Game gameName)
		{
			var game = gameRepository.GetGameByName(gameName.name);
			return View("GameDescription", game);
		}

    }
}
