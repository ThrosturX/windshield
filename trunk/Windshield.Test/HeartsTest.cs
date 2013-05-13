using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windshield.Common;
using Windshield.Games.Hearts;
using Windshield.Models.Common.Exceptions;
using Windshield.Models.Games;
using Windshield.Test.MockObjects;
using Windshield.Models;

using HPlayer = Windshield.Games.Hearts.Player;

namespace Windshield.Test
{

	[TestClass]
	public class HeartsTest
	{
		private MockUserRepo usr;
		private MockBoardRepo brd;
		private Hearts game;

		[TestInitialize]
		public void Setup()
		{
			// Arrange
			usr = new MockUserRepo();
			brd = new MockBoardRepo();

			List<User> players = new List<User>();
			User  a = new User();
			User  b = new User();
			User  c = new User();
			User  d = new User();
			a.UserName = "North";
			b.UserName = "South";
			c.UserName = "East";
			d.UserName = "West";
			usr.AddUser(a);
			usr.AddUser(b);
			usr.AddUser(c);
			usr.AddUser(d);
			players.Add(a);
			players.Add(b);
			players.Add(c);
			players.Add(d);

			game = new Hearts(players);

			brd.AddBoard(game.board);
		}

		[TestMethod]
		public void CheckingWhetherPlayersAreDealtRandomCards()
		{
			CardDeck deck = new CardDeck();

			for (int i = 0; i<4; ++i)
			{
				if (deck.GetCards(13).Equals(game.players[i].hand.ToList()))
					Assert.Fail();
			}
		}

		[TestMethod]
		public void CheckStartingPlayerHasTwoOfClubs()
		{
			HPlayer starter = new HPlayer();
			bool failMe = true;

			starter = game.GetStartingPlayer(null);

			foreach (var card in starter.hand)
			{
				if (card.suit == Suit.Club && card.face == 2)
					failMe = false;
			}

			if (failMe)
				Assert.Fail();

		}

		[TestMethod]
		public void CheckIfFirstTurnIsCorrect()
		{
			Assert.AreEqual(game.GetStartingPlayer(null), game.turn);
		}

		[TestMethod]
		public void CheckIncrement()
		{
			HPlayer first = game.turn;

			game.IncrementTurn();

			Assert.AreNotEqual(first, game.turn);
		}

		[TestMethod]
		public void EnsureTurnsCoverAllPlayersInSomeOrder()
		{
			HPlayer [] playerReference = new HPlayer[4];

			HPlayer first = game.turn;

			for (int i = 0; i < 4; ++i)
			{
				playerReference[i] = game.turn;
				game.IncrementTurn();
			}

			Assert.AreEqual(first, game.turn);

			// check if any player is twice in the reference array
			for (int i = 0; i < 4; ++i)
			{
				for (int j = 0; j < 4; ++j)
				{
					if (i != j)
					{
						Assert.AreNotEqual(game.players[i], playerReference[j]);
					}
				}
			}

		}

		[TestMethod]
		public void CreatingALargeHeartsGroupThrowsTooManyPlayersException()
		{
			try
			{
				List<User> players = new List<User>();
				User sally = new User();
				User jonas = new User();
				User timmy = new User();
				User kenny = new User();
				User homer = new User();
				sally.UserName = "Sally";
				jonas.UserName = "Jonas";
				timmy.UserName = "Timmy";
				kenny.UserName = "Kenny";
				homer.UserName = "D'oh!";
				usr.AddUser(sally);
				usr.AddUser(jonas);
				usr.AddUser(timmy);
				usr.AddUser(kenny);
				usr.AddUser(homer);
				players.Add(sally);
				players.Add(jonas);
				players.Add(timmy);
				players.Add(kenny);
				players.Add(homer);

				brd.AddBoard(game.board);

				game = new Hearts(players);
				Assert.Fail();
			}
			catch (TooManyPlayersException) 
			{
				// intentionally empty
			}
			catch (Exception)
			{
				Assert.Fail();
			}
		}

		[TestMethod]
		public void CheckIfTrickIsCreated()
		{
			HPlayer p = game.GetStartingPlayer(null);
			Card r = null;
			foreach (var card in p.hand)
			{
				if (card.suit == Suit.Club && card.face == 2)
				{
					r = card;
				}
			}

			Assert.AreEqual(true, game.PlayCard(p, r));

			Assert.AreNotEqual(null, r);
			Assert.AreNotEqual(null, game.trick);
		}
	}
}
