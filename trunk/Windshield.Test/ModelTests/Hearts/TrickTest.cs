using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windshield.Models.Games;
using Windshield.Common;
using System.Collections.Generic;

namespace Windshield.Test.ModelTests
{
	[TestClass]
	public class TrickTest
	{
		public Trick trick;
		public HPlayer n, w, s, e;
		[TestInitialize]
		public void Setup()
		{
			n = new HPlayer();
			w = new HPlayer();
			s = new HPlayer();
			e = new HPlayer();
			trick = new Trick(w, new Card(2, Suit.Club));
			trick.AddCard(s, new Card(3, Suit.Club));
			trick.AddCard(e, new Card(Rank.Queen, Suit.Spade));
			trick.AddCard(n, new Card(Rank.Ten, Suit.Club));
		}
		
		[TestMethod]
		public void TrickSeeIfNorthClaimsTrick()
		{
			Assert.AreEqual(trick.GetPlayerByCard(new Card(Rank.Ten, Suit.Club)), trick.GetClaimer().Key);
		}

		[TestMethod]
		public void TrickSeeIfClaimerGets13Points()
		{
			Assert.AreEqual(13, trick.GetClaimer().Value);
		}

		[TestMethod]
		public void TrickClaimTwoHeartsForTwoPoints()
		{
			Trick twoHearts = new Trick(n, new Card(Rank.Ace, Suit.Spade));
			twoHearts.AddCard(w, new Card(Rank.Jack, Suit.Heart));
			twoHearts.AddCard(s, new Card(5, Suit.Spade));
			twoHearts.AddCard(e, new Card(2, Suit.Heart));

			KeyValuePair<HPlayer, int> result = new KeyValuePair<HPlayer, int>();

			result = twoHearts.GetClaimer();

			Assert.AreEqual(2, result.Value);
			Assert.AreEqual(n, result.Key);
		}
	}
}
