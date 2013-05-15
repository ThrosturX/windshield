using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Windshield.Models;
using Windshield.Models.Games.TicTacToe;
using Windshield.Models.Games.Hearts;
using Windshield.ViewModels;

namespace Windshield.Controllers
{
    public class GamesController : Controller
    {
        private IBoardRepo boardRepo = null;
		private IGameRepo gameRepo = null;
		private IUserRepo userRepo = null;
		private LiveInstanceRepo liveRepo = null;

		public GamesController()
		{
			boardRepo = new BoardRepo();
			gameRepo = new GameRepo();
			userRepo = new UserRepo();
			liveRepo = new LiveInstanceRepo();
		}

		public GamesController(BoardRepo bRep, IGameRepo gRep, IUserRepo uRep)
		{
			boardRepo = bRep;
			gameRepo = gRep;
			userRepo = uRep;
			liveRepo = new LiveInstanceRepo();
		}

        public ActionResult Index()
        {
			return View();
        }

/*
		// play against computer
		[Authorize]
		public ActionResult TicTacToes()
		{
			List<User> players = new List<User>();
			User playerOne = userRepo.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());
			players.Add(playerOne);
			TicTacToe gameBoard = new TicTacToe(players);
			Board board = new Board();
			board.status = gameBoard.GetStatus();
			board.idGame = 2; // TODO: FIX
			board.startDate = DateTime.Now;
			boardRepo.AddBoard(board);
			boardRepo.Save();
			gameBoard.id = board.id;

			GameInstance theGame = new GameInstance(gameBoard, board);

			return View("Index", theGame);
		}
*/

		// Creates a tictactoe game
		[Authorize]
		public ActionResult TicTacToe(int? targetId)
		{
			if (targetId == null)
			{
				List<User> players = new List<User>();
				User playerOne = userRepo.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());
				players.Add(playerOne);
				TicTacToe gameBoard = new TicTacToe(players);
				Board board = new Board();
				board.status = gameBoard.GetStatus();
				board.idGame = 2; // TODO: FIX
				board.startDate = DateTime.Now;
				boardRepo.AddBoard(board);
				boardRepo.Save();
				gameBoard.id = board.id;

				GameInstance theGame = new GameInstance(gameBoard, board);

				return View("Index", theGame);
			}
			else
			{

				Board board = boardRepo.GetBoardById((int)targetId);

				IQueryable<User> userlist = boardRepo.GetBoardUsers(board);

				List<User> players = userlist.ToList();
				TicTacToe gameBoard = new TicTacToe(players);
				gameBoard.id = (int)targetId;
				board.status = gameBoard.GetStatus();

				GameInstance theGame = new GameInstance(gameBoard, board);

				liveRepo.Add(theGame);

				return View("Index", theGame);
			}
		}

		[Authorize]
		public ActionResult JoinGame(int targetId)
		{
			GameInstance theGame = liveRepo.GetInstanceByID(targetId);
			return View("Index", theGame);
		}

		[Authorize]
		public ActionResult JoinLobby(int targetId)
		{
			Board board = boardRepo.GetBoardById(targetId);
			User user = userRepo.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());

			// Check if the player is already in the selected game instance
			IQueryable<User> currentPlayers = boardRepo.GetBoardUsers(board);
			if (!currentPlayers.Contains(user))
			{
				// The player is not in the game instance, therefore he has to be added to it
				Player player = new Player();
				player.idBoard = board.id;
				player.UserName = user.UserName;
				boardRepo.AddPlayer(player);
				boardRepo.Save();
			}

			LobbyViewModel vm = new LobbyViewModel();
			vm.boardId = targetId;
			vm.guests = new List<User>();
			vm.guests.Add(user);

			return View("GameLobby", vm);
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

				return RedirectToAction("GameLobby", new { targetId = board.id });
			}
		}

		public ActionResult GameLobby(int targetId)
		{
			Board board = boardRepo.GetBoardById(targetId);
			IQueryable<User> lobbyGuests = boardRepo.GetBoardUsers(board);
			List<User> users = lobbyGuests.ToList();
		

			foreach (var user in users)
			{
				if (userRepo.GetGameRatingByGame(user, board.Game) == null)
				{
					GameRating rating = new GameRating();
					rating.idGame = board.Game.id;
					rating.userName = user.UserName;
					rating.rating = 1200;
					gameRepo.AddRating(rating);
					gameRepo.Save();
				}
			}
			LobbyViewModel vm = new LobbyViewModel();
			vm.boardId = targetId;
			vm.guests = users;

			return View("GameLobby", vm);
		}

		/// <summary>
		/// Controller that returns the view that displays all boards for a game
		/// </summary>
		public ActionResult Boards(Game game)
		{
			// EDIT: 15 - 11:00 - Ragnar and Bjorn
			var viewModel = new BoardTableViewModel(game.name);
			foreach (Board board in boardRepo.GetBoards(gameRepo.GetGameByName(game.name)))
			{
				viewModel.Add(board);
			}
			return View("Boards", viewModel);
		}

		/// <summary>
		/// Controller that returns the view that displays all the boards that user is playing in
		/// </summary>
		public ActionResult MyGames()
		{
			// EDIT: 15 - 11:00 - Ragnar and Bjorn

			User user = userRepo.GetUserByName(User.Identity.Name);
			var viewModel = new BoardTableViewModel();
			foreach (Board board in boardRepo.GetBoards(user))
			{
				viewModel.Add(board);
			}
			return View("MyGames", viewModel);
		}

		public JsonResult GetPlayersInGameLobby(Board board)
		{

			List<GameLobbyViewModel> model = new List<GameLobbyViewModel>();
			// note that the only property board has is id, but that should be sufficient
			var users = boardRepo.GetBoardUsers(board);
			foreach (var user in users)
			{
				GameLobbyViewModel m = new GameLobbyViewModel();
				m.UserName = user.UserName;
				model.Add(m);
			}
			return Json(model, JsonRequestBehavior.AllowGet);
		}

    }
}

				// RedirectToAction
				// TODO: Latebindinglol
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