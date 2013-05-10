using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Common
{
	public enum Rank
	{
		Joker = 0, Ace, Ten = 10, Jack = 11, Queen = 12, King = 13
	}

	public enum Suit
	{
		Joker, Heart, Spade, Diamond, Club
	}
	public class Card
	{
		public int face { get; set; }
		public Suit suit { get; set; }

		public Card()
		{
			face = 0;
			suit = Suit.Joker;
		}

		public Card(int _face)
		{
			face = _face;
			suit = Suit.Joker;
		}

		public Card(Suit _suit)
		{
			face = 0;
			suit = _suit;
		}

		public Card(int _face, Suit _suit)
		{
			face = _face;
			suit = _suit;
		}

		public Card(Rank _face, Suit _suit)
		{
			face = (int)_face;
			suit = _suit;
		}

		public override string ToString()
		{
			string card_string;
			switch (face)
			{
				case 1:
					card_string = "Ace";
					break;
				case 11:
					card_string = "Jack";
					break;
				case 12:
					card_string = "Queen";
					break;
				case 13:
					card_string = "King";
					break;
				default:
					card_string = "Joker";
					break;
			}

			if (face >= 2 && face <= 10)
				card_string = face.ToString();

			card_string = card_string + " of " + suit.ToString() + "s";

			if (card_string.Contains("Joker"))
				card_string = "Joker";

			return card_string;
		}
	}

	
}