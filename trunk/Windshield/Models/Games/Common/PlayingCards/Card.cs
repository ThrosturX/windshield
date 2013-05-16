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
			face = (int)Rank.Joker;
			suit = Suit.Joker;
		}

		public Card(int _face)
		{
			face = _face;
			suit = Suit.Joker;
		}

		public Card(Suit _suit)
		{
			face = (int)Rank.Joker;
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

		public bool IsJoker()
		{
			if (face == 0 && suit == Suit.Joker)
			{
				return true;
			}

			return false;
		}

		public int CardValue()
		{
			// Used to sort by suits
			int theSuit = 500 - (int)suit*100;
			int theFace = face;
			// Hotfix to ensure that aces are higher than kings
			if (theFace == 1)
			{
				theFace = 14;
			}

			return theSuit + theFace;
		}

		public static Card SpawnCardFromString(String str)
		{
			Card card = new Card();

			switch (str[0])
			{
				case 'A':
					{
						card.face = (int)Rank.Ace;
						break;
					}
				case 'T':
					{
						card.face = (int)Rank.Ten;
						break;
					}
				case 'J':
					{
						card.face = (int)Rank.Jack;
						break;
					}
				case 'Q':
					{
						card.face = (int)Rank.Queen;
						break;
					}
				case 'K':
					{
						card.face = (int)Rank.King;
						break;
					}
				default:
					{
						int a = 0;
						int.TryParse(str[0].ToString(), out a);
						card.face = a;
						break;
					}
			}

			switch (str[1])
			{
				case 'H':
					{
						card.suit = Suit.Heart;
						break;
					}
				case 'S':
					{
						card.suit = Suit.Spade;
						break;
					}
				case 'D':
					{
						card.suit = Suit.Diamond;
						break;
					}
				case 'C':
					{
						card.suit = Suit.Club;
						break;
					}
				default:
					{
						card.suit = Suit.Joker; // shouldn't happen
						break;
					}
			}

			return card;
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

			if (card.IsJoker())
			{
				return "  ";
			}

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
						str += "S";
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

		public bool IsEqual(Card that)
		{
			if (this.suit == that.suit && this.face == that.face)
			{
				return true;
			}

			return false;
		}

	}
}
