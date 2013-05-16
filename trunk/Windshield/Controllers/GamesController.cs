using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Windshield.Models;
using Windshield.Models.Games;
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

		public GamesController()
		{
			boardRepo = new BoardRepo();
			gameRepo = new GameRepo();
			userRepo = new UserRepo();
		}

		public GamesController(IBoardRepo bRepo, IGameRepo gRepo, IUserRepo uRepo)
		{
			boardRepo = bRepo;
			gameRepo = gRepo;
			userRepo = uRepo;
		}

        public ActionResult Index(int idBoard)
        {
			Board board = boardRepo.GetBoardById(idBoard);

			if (board == null)
			{
				return View("Error");
			}

			BoardViewModel viewModel = new BoardViewModel(board);
			viewModel.AddPlayers(boardRepo.GetBoardUsers(board).ToList());
			return View("Index", viewModel);
        }

		[Authorize]
		// [HttpPost]
		public ActionResult NewBoard(string gameName)
		{
			// get game
			Game game = gameRepo.GetGameByName(gameName);

			// check if game exists
			if (game == null)
			{
				return View("Error");
			}
			else
			{
				// gets the current User
				User user = userRepo.GetUserByName(User.Identity.Name);
				// rates the user if he isn't rated
				if (userRepo.GetGameRatingByGame(user, game) == null)
				{
					GameRating rating = new GameRating();
					rating.idGame = game.id;
					rating.userName = user.UserName;
					rating.rating = 1200;  // supposed to be redundant, but seemed to be necessary
					gameRepo.AddRating(rating);
					gameRepo.Save();
				}

				// create a new board and save it in the database
				Board board = new Board();
				board.idGame = game.id;
				board.ownerName = user.UserName;
				boardRepo.AddBoard(board);
				boardRepo.Save();

				// create a Player that associates the 'owner' with the board
				Player player = new Player();
				player.dateJoined = DateTime.Now;
				player.UserName = user.UserName;
				player.idBoard = board.id;
				boardRepo.AddPlayer(player);
				boardRepo.Save();

				// redirect to a new game lobby
				return RedirectToAction("GameLobby", new { idBoard = board.id });
			}
		}

		public ActionResult GameLobby(int idBoard)
		{
			// get board and check if it exists
			Board board = boardRepo.GetBoardById(idBoard);
			if (board == null)
			{
				return View("Error");
			}

			// gets all users associated with the board
			List<User> users = boardRepo.GetBoardUsers(board).ToList();
			if (users == null)
			{
				// this should never happen
				return View("Error");
			}
			LobbyViewModel viewModel = new LobbyViewModel();
			viewModel.boardId = idBoard;
			viewModel.guests = users;
			viewModel.Image = board.Game.image;
			viewModel.theName = board.Game.name;

			return View("GameLobby", viewModel);
		}


		[Authorize]
		public ActionResult JoinLobby(int idBoard)
		{
			Board board = boardRepo.GetBoardById(idBoard);
			if (board == null)
			{
				return View("Error");
			}

			bool gameIsFull = board.Players.Count() >= board.Game.maxPlayers;
			if (gameIsFull)
			{
				return View("Error");
			}

			User user = userRepo.GetUserByName(User.Identity.Name);
			IQueryable<User> currentPlayers = boardRepo.GetBoardUsers(board);

			// rates the user if he isn't rated
			if (userRepo.GetGameRatingByGame(user, board.Game) == null)
			{
				GameRating rating = new GameRating();
				rating.idGame = board.Game.id;
				rating.userName = user.UserName;
				rating.rating = 1200;  // supposed to be redundant, but seemed to be necessary
				gameRepo.AddRating(rating);
				gameRepo.Save();
			}

			// Check if the player is already in the selected game instance
			if (!currentPlayers.Contains(user))
			{
				// TODO: Make a seperate POST method
				// Ensure the game is not full before trying to insert the player
				// The player is not in the game instance, therefore he has to be added to it
					Player player = new Player();
					player.idBoard = board.id;
					player.UserName = user.UserName;
					boardRepo.AddPlayer(player);
					boardRepo.Save();
					
			}
			// Send the player to the game lobby
			/*LobbyViewModel vm = new LobbyViewModel();
			vm.boardId = idBoard;
			vm.Image = board.Game.image;
			vm.theName = board.Game.name;
			vm.guests = new List<User>();
			vm.guests.Add(user); */

			return RedirectToAction("GameLobby", new { idBoard = board.id });
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

		public ActionResult StartGame(int idBoard)
		{
			Board board = boardRepo.GetBoardById(idBoard);

			if (board == null)
			{
				return View("Error");
			}

			if (board.ownerName != User.Identity.Name)
			{
				// make this more gentle
				return View("Error");
			}

			// TODO: late binding
			IGame iGame;
			switch (board.Game.model)
			{
				case "TicTacToe":
					iGame = new TicTacToe();
					break;
				// TODO: Case Hearts	
				case "Hearts":
					iGame = new Hearts();
					break;

				default:
					return View("Error");
			}
			BoardViewModel viewModel = new BoardViewModel(board);
			viewModel.AddPlayers(boardRepo.GetBoardUsers(board).ToList());

			iGame.AddPlayers(viewModel.GetPlayers(board.Game.maxPlayers));
			board.status = iGame.GetStatus();
			board.startDate = DateTime.Now;
			boardRepo.Save();

			return RedirectToAction("Index", new { idBoard = board.id });


		}


		/// <summary>
		/// Returns a view for the specified board if it is an ongoing board that the user has access to
		/// </summary>
		[Authorize]
		public ActionResult JoinGame(int idBoard)
		{
			// Ragnar and Oli

			// check if board exists
			Board board = boardRepo.GetBoardById(idBoard);
			bool boardExists = (board != null);
			if (!boardExists)
			{
				return View("Error");
			}

			// check if user is a player in board
			bool isPlayer = false;
			var players = boardRepo.GetBoardUsers(board);
			foreach (User player in players)
			{
				if (player.UserName == User.Identity.Name)
				{
					isPlayer = true;
					break;
				}
			}

			// return view if user is a player on board and game is ongoing
			if (isPlayer && (board.startDate != null) && (board.endDate == null))
			{
				var viewModel = new BoardViewModel(board);
				viewModel.AddPlayers(players.ToList());
				return RedirectToAction("Index", viewModel);
			}
			else
			{
				return View("Error");
			}
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