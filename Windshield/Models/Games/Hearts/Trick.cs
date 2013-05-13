using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Common;

namespace Windshield.Games.Hearts
{
	public class Trick : List<KeyValuePair<Player, Card>>
	{
		public Suit leader { get; set; }
		public Player claimer { get; set; }
		public int points { get; set; }

		/// <summary>
		/// Creates a new trick with a leading suit specified by card
		/// </summary>
		/// <param name="card">The card played to start the trick</param>
		public Trick(Player player, Card card)
		{
			KeyValuePair<Player, Card> pair = new KeyValuePair<Player, Card>(player, card);
			leader = card.suit;
			Add(pair);
			claimer = player;
		}

		public void AddCard(Player player, Card card)
		{
			this.Add(new KeyValuePair<Player, Card>(player, card));
		}

		public void CalculatePoints()
		{
			Card QofSpades = new Card(Rank.Queen, Suit.Spade);
			points = 0;
			foreach (var card in this)
			{
				if (card.Value.suit == Suit.Heart)
				{
					points += 1;
				}
				else if (card.Value.Equals(QofSpades))
				{
					points += 13;
				}
			}
		}

		public Player GetPlayerByCard(Card card)
		{
			foreach (var pair in this)
			{
				if (pair.Value.Equals(card))
				{
					return pair.Key;
				}
			}

			return null; // no player found!
		}

		public KeyValuePair<Player, int> GetClaimer()
		{
			// check how many points the trick is worth
			CalculatePoints();

			Card highest = new Card(leader);

			// find the player who should claim the trick
			foreach (var card in this)
			{
				if ((card.Value.suit == leader) && (card.Value.face > highest.face))
				{
					highest = card.Value;
				}
			}

			claimer = GetPlayerByCard(highest);

			KeyValuePair<Player, int> trick = new KeyValuePair<Player, int>(claimer, points);

			return trick;
		}

	}
}