using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windshield.Models;
using Windshield.Models.Games;
using Windshield.ViewModels;

namespace Windshield.Controllers
{
	[Authorize]
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

		[Authorize]
        public ActionResult Index(int idBoard)
        {
			Board board = boardRepo.GetBoardById(idBoard);

			if (board == null)
			{
				// the board does not exist
				return View("Error", new ErrorViewModel("Board", "Board does not exist."));
			}

			if (board.endDate != null)
			{
				// board has ended
				return View("Error", new ErrorViewModel("Board", "The board is no longer active."));
			}

			User user = userRepo.GetUserByName(User.Identity.Name);
			if (!boardRepo.GetBoardUsers(board).Contains(user))
			{
				// user is not a player on this board
				return View("Error", new ErrorViewModel("Board", "You are not a player on this board."));
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
				return View("Error", new ErrorViewModel("Game", "No game exists with this name."));
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
				return View("Error", new ErrorViewModel("Board", "Board does not exist."));
			}

			// gets all users associated with the board
			List<User> users = boardRepo.GetBoardUsers(board).ToList();
			if (users == null)
			{
				// this should never happen
				return View("Error", new ErrorViewModel("Board", "There are no players associated with this board."));
			}
			LobbyViewModel viewModel = new LobbyViewModel();
			viewModel.boardId = idBoard;
			viewModel.guests = users;
			viewModel.Image = board.Game.image;
			viewModel.theName = board.Game.name;

			return View("GameLobby", viewModel);
		}


		[Authorize]
		public ActionResult JoinBoard(int idBoard)
		{
			Board board = boardRepo.GetBoardById(idBoard);
			if (board == null)
			{
				// board does not exist
				return View("Error", new ErrorViewModel("Board", "Board does not exist."));
			}

			bool gameIsFull = board.Players.Count() >= board.Game.maxPlayers;
			if (gameIsFull)
			{
				return View("Error", new ErrorViewModel("Board", "There is no room for more players on this board."));
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

			if (board.startDate == null)
			{
				return RedirectToAction("GameLobby", new { idBoard = board.id });
			}
			else
			{
				return RedirectToAction("Index", new { idBoard = board.id });
			}

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
				// board does not exist
				return View("Error", new ErrorViewModel("Board", "Board does not exist."));
			}

			if (board.ownerName != User.Identity.Name)
			{
				// the user tried to start a game he does not own
				return View("Error", new ErrorViewModel("Board", "You are not the owner of this board."));
			}

			IGame iGame = IGameBinder.GetGameObjectFor(board.Game.model);
			BoardViewModel viewModel = new BoardViewModel(board);
			viewModel.AddPlayers(boardRepo.GetBoardUsers(board).ToList());
			
			// adds the correct number of players to the game
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
				// board does not exist
				return View("Error", new ErrorViewModel("Board", "Board does not exist."));
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
			if (!isPlayer)
			{
				return View("Error", new ErrorViewModel("Board", "You are not a player on this board."));
			}

			
			
			
			if (board.endDate != null)
			{
				return View("Error", new ErrorViewModel("Board", "This board is no longer active."));
				// board is no longer active
			}


			if (board.startDate == null)
			{
				return RedirectToAction("GameLobby", idBoard);
			}



			return RedirectToAction("Index", idBoard);
		}

		/// <summary>
		/// Controller that returns the view that displays all boards for a game
		/// </summary>
		public ActionResult Boards(Game game)
		{
			// EDIT: 17 - 00:30 - Ragnar and Elin
			
			var viewModel = new BoardTableViewModel(game.name);
			var result = from boards in boardRepo.GetBoards(gameRepo.GetGameByName(game.name))
						 where boards.startDate == null
						 select boards;
			viewModel.Add(result);
			return View("Boards", viewModel);
		}

		/// <summary>
		/// Controller that returns the view that displays all the boards that user is associated with
		/// </summary>
		public ActionResult MyBoards()
		{
			User user = userRepo.GetUserByName(User.Identity.Name);
			var viewModel = new List<BoardTableViewModel>();
			var userBoards = boardRepo.GetBoards(user);
			var ongoing = from boards in userBoards
						  where (boards.startDate != null) && (boards.endDate == null)
						  select boards;
			var inLobby = from boards in userBoards
						  where boards.startDate == null
						  select boards;
			viewModel.Add(new BoardTableViewModel(ongoing));
			viewModel.Add(new BoardTableViewModel(inLobby));
			return View("MyBoards", viewModel);
		}
    }
}
