using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Common;

namespace Windshield.Models.Games
{
	public class Trick : List<KeyValuePair<HPlayer, Card>>
	{
		public Suit leader { get; set; }
		public HPlayer claimer { get; set; }
		public int points { get; set; }

		/// <summary>
		/// Shouldn't be used except with SetStatus! Creates a goofy trick
		/// </summary>
		public Trick()
		{
			leader = Suit.Joker;
			claimer = null;
			points = 0;
		}

		/// <summary>
		/// Creates a new trick with a leading suit specified by card
		/// </summary>
		/// <param name="card">The card played to start the trick</param>
		public Trick(HPlayer player, Card card)
		{
			KeyValuePair<HPlayer, Card> pair = new KeyValuePair<HPlayer, Card>(player, card);
			leader = card.suit;
			Add(pair);
			claimer = player;
		}

		public void AddCard(HPlayer player, Card card)
		{
			this.Add(new KeyValuePair<HPlayer, Card>(player, card));
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
				else if (card.Value.IsEqual(QofSpades))
				{
					points += 13;
				}
			}
		}

		public HPlayer GetPlayerByCard(Card card)
		{
			foreach (var pair in this)
			{
				Card cmp = pair.Value;
				if (cmp.face == card.face && cmp.suit == card.suit)
				{
					return pair.Key;
				}
			}

			return null; // no player found!
		}

		public KeyValuePair<HPlayer, int> GetClaimer()
		{
			// check how many points the trick is worth
			CalculatePoints();

			Card highest = new Card(leader);

			// find the player who should claim the trick
			foreach (var card in this)
			{
				if ((card.Value.suit == leader) && (card.Value.face == (int)Rank.Ace))
				{
					highest = card.Value;
					break;
				}

				if ((card.Value.suit == leader) && (card.Value.face > highest.face))
				{
					highest = card.Value;
				}
			}

			claimer = GetPlayerByCard(highest);

			KeyValuePair<HPlayer, int> trick = new KeyValuePair<HPlayer, int>(claimer, points);

			return trick;
		}

		public Card GetCardAtIndex(int index)
		{
			KeyValuePair<HPlayer, Card> pair;

			try
			{
				pair = this.ElementAt(index);
			}
			catch (Exception)
			{
				return new Card();
			}
			if (pair.Value == null)
			{
				return new Card(Rank.Joker, Suit.Joker);
			}

			return pair.Value;
		}

		public string GetPlayerNameAtIndex(int index)
		{
			KeyValuePair<HPlayer, Card> pair;

			try
			{
				pair = this.ElementAt(index);
			}

			catch(Exception)
			{
				return " ";
			}

			return pair.Key.user.UserName;
		}

	}
}