using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Windshield.Controllers;
using System.Web.Mvc;
using Windshield.Test.MockObjects;
using Windshield.Models;
using Windshield.ViewModels;

namespace Windshield.Test.ControllersTest
{
	[TestClass]
	public class GamesControllerTest
	{
		private GamesController games;
		private MockBoardRepo boardRepoMock;
		private MockGameRepo gameRepoMock;
		private MockUserRepo userRepoMock;
	
		[TestInitialize]
		public void Setup()
		{
			// Arrange

			boardRepoMock = new MockBoardRepo();
			gameRepoMock = new MockGameRepo();
			userRepoMock = new MockUserRepo();

			List<User> Players = new List<User>();
			User p1 = new User();
			System.Guid a = new Guid();

			p1.UserName = "player1";
			p1.UserId = a;
			userRepoMock.AddUser(p1);
			Players.Add(p1);			
			
			games = new GamesController(boardRepoMock, gameRepoMock, userRepoMock);

			LiveInstanceRepo liverepo = new LiveInstanceRepo();
		}

/*
		[TestMethod]
		public void CheckTicTacToehasId()
		{
			Board b = new Board();

			

			b.id = (15);
			Player player1 = new Player();
			player1.User = userRepo.GetUserByName("player1");
			player1.idBoard = b.id;
			b.ownerName = "player1";
			boardRepo.AddBoard(b);
			boardRepo.AddPlayer(player1);
			boardRepo.playerTable.Add(player1);

			ViewResult result = games.TicTacToe(b.id) as ViewResult;

			Assert.AreEqual(15, b.id, "BoardIdIsNotEqual");
		}
  */

/*		[TestMethod]
		public void GameControllerJoinLobbyTicTacAddingPlayerToEmptyBoard()
		{
			Game game = CreateGame(1,2);
			Board board = CreateBoard(1,1);
			board.Game = game;
			boardRepoMock.AddBoard(board);
			User user = CreateUser("user1");

			IQueryable<User> usersinboard = boardRepoMock.GetBoardUsers(board);

			if (!usersinboard.Contains(user))
			{
				if (usersinboard.Count() < board.Game.maxPlayers)
				{
					Player player = CreatePlayer(user.UserName, board.id);
					boardRepoMock.AddPlayer(player);

					Assert.AreEqual(player.idBoard, board.id, "The player is not in the board lobby");
					
				}

				Assert.IsTrue(usersinboard.Count() < board.Game.maxPlayers);
			}

			Assert.IsTrue(!usersinboard.Contains(user));
		}

		[TestMethod]
		public void GameControllerJoinLobbyTicTacToeAddingPlayerToFullBoard()
		{ 
			//making a board for tictactoetestgame
			Game game = CreateGame(1,2);
			Board board = CreateBoard(1,1);
			board.Game = game;
			boardRepoMock.AddBoard(board);

			//making 2 players assigned to that board.
			User firstuser = CreateUser("firstuser");
			User seconduser = CreateUser("seconduser");

			Player firstplayer = CreatePlayer(firstuser.UserName, board.id);
			Player secondplayer = CreatePlayer(seconduser.UserName, board.id);
			//adding the players to the board.
			boardRepoMock.AddPlayer(firstplayer);
			boardRepoMock.AddPlayer(secondplayer);


			User user = CreateUser("user1");


			IQueryable<User> usersinboard = boardRepoMock.GetBoardUsers(board);

			if (!usersinboard.Contains(user))
			{
				if (usersinboard.Count() < board.Game.maxPlayers)
				{
				
				}

				Assert.IsFalse(usersinboard.Count() < board.Game.maxPlayers);
			}

			Assert.IsTrue(!usersinboard.Contains(user));
			
		}

		/*
		[TestMethod]
		public void GameControllerJoinLobbyTicTacToeAlreadyInLobby()
		{ 
			
			//making a board for tictactoetestgame
			Game game = CreateGame(1,2);
			Board board = CreateBoard(1,1);
			board.Game = game;
			boardRepoMock.AddBoard(board);
			
			User user = CreateUser("user1");

			//adding the player to the board.
			Player player = CreatePlayer(user.UserName, board.id);
			boardRepoMock.AddPlayer(player);

			IQueryable<User> usersinboard = boardRepoMock.GetBoardUsers(board);

			if (!usersinboard.Contains(user))
			{
				if (usersinboard.Count() < board.Game.maxPlayers)
				{

				}

				Assert.IsTrue(usersinboard.Count() < board.Game.maxPlayers);
			}

			Assert.IsFalse(!usersinboard.Contains(user));	
		
		}
		*/




		//Creating a Testgame
		public Game CreateGame(int id, int maxPlayers)
		{
			Game game = new Game();
			game.id = id;
			game.name = "TestGame" + id;
			game.maxPlayers = maxPlayers;
			gameRepoMock.AddGame(game);

			return game;
		}

		//Creating a Testboard
		public Board CreateBoard(int id, int idGame)
		{
			Board board = new Board();
			board.id = id;
			board.idGame = idGame;
			return board;
		}

		//Creating a User
		public User CreateUser(string username)
		{
			User user = new User();
			user.UserName = username;
			System.Guid testerid = new Guid();
			user.UserId = testerid;
			userRepoMock.AddUser(user);

			return user;
		}

		//Creating a player
		public Player CreatePlayer(string playername, int id)
		{
			Player player = new Player();
			player.idBoard = id;
			player.UserName = playername;
			return player;
		}
	}
}
