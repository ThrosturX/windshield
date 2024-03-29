﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Common;

namespace Windshield.Models.Games
{
	public class Hand : List<Card>
	{
		public Hand(List<Card> cards)
		{
			foreach (var card in cards)
			{
				this.Add(card);
			}
		}

		public List<Card> ToList()
		{
			List<Card> ret = new List<Card>();
			foreach (var card in this)
			{
				ret.Add(card);
			}
			
			return ret;
		}

		public Card FindCard(int face, Suit suit)
		{
			foreach (var card in this)
			{
				if (card.face == face && card.suit == suit)
				{
					return card;
				}
			}

			return null; // card not found
		}

		public Card RandomCard()
		{
			Random rnd = new Random();

			int r = rnd.Next(Count);
			if (Count == 0)
			{
				return null;
			}

			return this[r];
		}

		public Card FindAnyInSuit(Suit suit)
		{
			foreach (var card in this)
			{
				if (card.suit == suit)
				{
					return card;
				}
			}

			return null;
		}

		public Card FindNotInSuit(Suit suit)
		{
			foreach (var card in this)
			{
				if (card.suit != suit)
				{
					return card;
				}
			}

			return null;
		}

		public Card FindPreferablyInSuit(Suit suit)
		{
			if (FindAnyInSuit(suit) == null)
			{
				return FindNotInSuit(suit);
			}
			else
			{
				return FindAnyInSuit(suit);
			}
		}

		public string CreateHandString()
		{
			List<string> cardList = new List<string>();
			foreach (var card in this)
			{
				cardList.Add(Card.CreateCardString(card));
			}

			for (int i = cardList.Count; i < 13; i++)
			{
				cardList.Add("  ");
			}

			string str = String.Join(",", cardList);

			return str;
		}
		
		// slightly unnecessary but more descriptive
		internal void RemoveCard(Card card)
		{
			this.Remove(FindCard(card.face, card.suit));
		}
	}
}