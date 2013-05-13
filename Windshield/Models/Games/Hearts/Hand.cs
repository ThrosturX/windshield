using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Common;

namespace Windshield.Games.Hearts
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
	}
}