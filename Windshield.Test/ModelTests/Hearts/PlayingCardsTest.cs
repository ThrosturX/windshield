using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windshield.Common;

namespace Windshield.Test.ModelTests
{
	[TestClass]
	public class PlayingCardsTest
	{
		public CardDeck deck;

		[TestInitialize]
		public void Setup()
		{
			deck = new CardDeck();
		}

		[TestMethod]
		public void CardCreatesCorrectStringAceOfSpades()
		{
			Assert.AreEqual("AS", Card.CreateCardString(new Card(Rank.Ace, Suit.Spade)));
		}

		[TestMethod]
		public void CardCreatesCorrectStringBunchOfCards()
		{
			Assert.AreEqual("QH", Card.CreateCardString(new Card(Rank.Queen, Suit.Heart)));
			Assert.AreEqual("JC", Card.CreateCardString(new Card(Rank.Jack, Suit.Club)));
			Assert.AreEqual("2H", Card.CreateCardString(new Card(2, Suit.Heart)));
			Assert.AreEqual("AD", Card.CreateCardString(new Card(Rank.Ace, Suit.Diamond)));
			Assert.AreEqual("  ", Card.CreateCardString(new Card()));
			Assert.AreEqual("7S", Card.CreateCardString(new Card(7, Suit.Spade)));
		}
	}
}
