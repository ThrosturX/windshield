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
		}

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
