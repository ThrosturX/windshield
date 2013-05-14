using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Windshield.Models;
using Windshield.Models.Games.TicTacToe;
using Windshield.Models.Games.Hearts;

namespace Windshield.Controllers
{
    public class GamesController : Controller
    {
        private IBoardRepo boardRepo = null;
		private IGameRepo gameRepo = null;
		private IUserRepo userRepo = null;

		public GamesController()
		{
			boardRepo = new BoardRepo();
			gameRepo = new GameRepo();
			userRepo = new UserRepo();
		}

		public GamesController(BoardRepo bRep, IGameRepo gRep, IUserRepo uRep)
		{
			boardRepo = bRep;
			gameRepo = gRep;
			userRepo = uRep;
		}

        public ActionResult Index()
        {
			return View();
		//	return RedirectToAction("Index", "Home");
        }

		// play against computer
		[Authorize]
		public ActionResult TicTacToe()
		{
			List<User> players = new List<User>();
			User playerOne = userRepo.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());
			players.Add(playerOne);
//			User playerTwo = userRepository.GetUserByName("banana");
//			players.Add(playerTwo);
			TicTacToe gameBoard = new TicTacToe(players);
			Board board = new Board();
			board.status = gameBoard.GetStatus();
			board.idGame = 2; // TODO: FIX
			boardRepo.AddBoard(board);
			boardRepo.Save();
			gameBoard.id = board.id;

			GameInstance theGame = new GameInstance(gameBoard, board);

			return View("Index", theGame);
		}

		[Authorize]
		//[HttpPost]
		public ActionResult NewBoard(string gameName)
		{
			User owner = userRepo.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());

			Game game = gameRepo.GetGameByName(gameName);
			if (game == null)
			{
				return View("Error");
			}
			else
			{
				// Create a new board and save it in the database
				Board board = new Board();
				board.idGame = game.id;
				board.ownerName = owner.UserName;
				boardRepo.AddBoard(board);
				boardRepo.Save();

				// Create a Player that associates the 'owner' with the board
				Player player = new Player();
				player.dateJoined = DateTime.Now;
				player.UserName = owner.UserName;
				player.idBoard = board.id;
				player.playerNumber = 0; //owner is player0
				boardRepo.AddPlayer(player);
				boardRepo.Save();

				return View("GameLobby");
				// RedirectToAction

				/*	owner.Players.Clear();
					Player player = new Player();
					player.dateJoined = DateTime.Now;
					player.playerNumber = 0;

					game.Boards.Add(board);
					board.Players.Add(player);

					owner.Players.Add(player);

					boardRepo.Save();
					foreach (var f in owner.Players)
					{
						//if(f.idUser == owner.UserId && f.Board.Game.id == game.id)
						System.Diagnostics.Debug.WriteLine(f.dateJoined);
					}
					return View("GameLobby");
					// RedirectToAction
				}

				/*
				 * Assembly executingAssembly = Assembly.GetExecutingAssembly();
					Type gameType = executingAssembly.GetType("Windshield.Models.Games." + game.model);

					//object gameInstance = Activator.CreateInstance(gameType); <-- óþarfi
					MethodInfo getFullNameMethod = gameType.GetMethod("GetFullName");


					ConstructorInfo ctor = gameType.GetConstructor(new[] { typeof(List<User>) });
					object gameInstance = ctor.Invoke(new object[] { players });
				*/
			}
		}

		public ActionResult GameLobby(Game game)
		{
			return View("GameLobby", game);
		}

		public ActionResult Boards(Game game)
		{
			var name = gameRepo.GetGameByName(game.name);
			var viewModel = boardRepo.GetBoards(name);
			return View("Boards", viewModel);
		}

		public ActionResult MyGames()
		{
			User user = userRepo.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());
			var model = boardRepo.GetBoards(user);
			return View("MyGames", model);
		}

    }
}
