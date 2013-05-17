using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windshield.Common;
using Windshield.Models;
using Windshield.Models.Games;
using Windshield.Test.ModelTests;

namespace Windshield.Test.ModelTests
{
	[TestClass]
	public class HandTest
	{
		Hearts game;
		[TestInitialize]
		public void Setup()
		{
			game = new Hearts();
			List<User> users = new List<User>();
			User a = new User();
			a.UserName = "banana";
			game.AddPlayers(users);

			CardDeck deck = new CardDeck();

			game.players[0].hand = new Hand(deck.GetCards(13));

		}

		[TestMethod]
		public void HandCheckForAceOfSpades()
		{
			Assert.AreEqual(null, game.players[0].hand.FindCard((int)Rank.Ace, Suit.Spade));
		}

		[TestMethod]
		public void HandCheckForTwoOfHearts()
		{
			if (!game.players[0].hand.FindCard(2, Suit.Heart).IsEqual(new Card(2, Suit.Heart)))
				Assert.Fail();
		}

		[TestMethod]
		public void HandRemoveTwoOfHearts()
		{
			game.players[0].hand.RemoveCard(new Card(2, Suit.Heart));

			Assert.AreEqual(null, game.players[0].hand.FindCard(2, Suit.Heart));
		}
	}
}
