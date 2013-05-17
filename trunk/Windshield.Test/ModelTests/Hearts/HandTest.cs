using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windshield.Common;
using Windshield.Models;
using Windshield.Models.Games;
using Windshield.Models.Games.Hearts;

namespace Windshield.Test.ModelTests.Hearts
{
	[TestClass]
	public class HandTest
	{
		Windshield.Models.Games.Hearts.Hearts game;
		[TestInitialize]
		public void Setup()
		{
			game = new Windshield.Models.Games.Hearts.Hearts();
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
