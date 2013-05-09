using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Common
{
	public class CardDeck : Queue<Card>
	{
		public Queue<Card> cards;

		// creates a fresh deck
		public CardDeck()
		{
			for (int i = 1; i <= 4; ++i)
			{
				for (int j = 1; j <= 13; ++j)
				{
					Card card = new Card(j, (Suit)i);
					Enqueue(card);
				}
			}
		}

		public Card Draw()
		{
			if (Count > 0)
			{
				return Dequeue();
			}
			else
			{
				return new Card(); // returns a Joker
			}
		}

		public List<Card> GetCards(int numCards)
		{
			List<Card> deal = new List<Card>();

			for (; numCards > 0; --numCards)
			{
				deal.Add(Draw());
			}

			return deal;
		}
		
		// removes a single card from the deck if it exists
		public void RemoveCard()
		{
			if (Count > 0)
				Dequeue();
		}

		public void Shuffle()
		{
			List<Card> list = new List<Card>(this);

			var shuffled = list.OrderBy(a => Guid.NewGuid());

			cards = new Queue<Card>(shuffled);

			Clear();

			foreach (var card in cards)
			{
				Enqueue(card);
			}
		}
	}
}