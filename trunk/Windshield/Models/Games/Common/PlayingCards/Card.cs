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

		public static Card Joker = new Card(Rank.Joker, Suit.Joker);

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


		/// <summary>
		/// Creates a two-character string representing a card
		/// </summary>
		/// <param name="card">the card to convert</param>
		/// <returns>A two character string representing the input</returns>
		/// <example>CreateCardString(new Card(2, Suit.Club)) returns "2C"</example>
		internal static string CreateCardString(Card card)
		{
			string str;

			// face
			switch (card.face)
			{
				case (int)Rank.Ace:
					{
						str = "A";
						break;
					}
				case (int)Rank.Ten:
					{
						str = "T";
						break;
					}
				case (int)Rank.Jack:
					{
						str = "J";
						break;
					}
				case (int)Rank.Queen:
					{
						str = "Q";
						break;
					}
				case (int)Rank.King:
					{
						str = "K";
						break;
					}
				default:
					{
						str = card.face.ToString();
						break;
					}
			}

			// Suit
			switch (card.suit)
			{
				case Suit.Heart:
					{
						str += "H";
						break;
					}
				case Suit.Spade:
					{
						str += "H";
						break;
					}
				case Suit.Diamond:
					{
						str += "D";
						break;
					}
				case Suit.Club:
					{
						str += "C";
						break;
					}
			}

			return str;
		}

	}
}
