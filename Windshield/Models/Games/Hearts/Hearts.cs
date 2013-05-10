using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Common;
using Windshield.Models;
using Windshield.Models.Common.Exceptions;
using Windshield.Games.Hearts;

namespace Windshield.Games.Hearts
{
	public class Hearts : Board
	{
		public static string name = "Hearts";
		public static string description =   "Hearts is an \"evasion-type\" trick-taking card game for four players. "
											+ "Hearts is also known as Black Lady, Chase the Lady, Crubs and Black Maria, "
											+ "though any of these may refer to the Heart-like, but differently-scored, "
											+ "game Black Lady. Hearts is regarded as a member of the Whist family of "
											+ "trick-taking games (which also includes Bridge and Spades), but Hearts "
											+ "is unique among Whist variants in that it is an evasion-type game.";

		public static List<string> rules = new List<string> {
										  "The Ace is the highest card,"
										, "Each player gets 13 cards"
										, "At the start of each match, all players give 3 cards to the player on his left(clockwise)"
										, "The player with the 2 of clubs starts the first round"
										, "Hearts and Queen of spades may not be played in the first round, unless there is no other option"
										, "Each player must follow the leading suit if possible"
										, "Highest card of the leading suit takes the trick and wins the round"
										, "The winner of a round starts the next round"
										, "Hearts may not be led until a heart or the Queen of spades has been played "
										+ "(this is called breaking hearts)"
										, "Queen of Spades can be lead at any time"
										, "Each heart in a trick gives 1 point for the holder."
										, "Queen of Spades gives 13 points for the holder"
										, "If a player holds all the hearts and the Queen of spades after a match, "
										+ "he receives 0 points, and 26 points are added on all opponents"
										, "When a player has reached 100 points, the game ends"
										, "The player with the lowest score wins"
										};

		public HeartsPlayer [] players;
		public CardDeck deck;

		/// <summary>
		/// Default constructor. Note that instances require players to be instantiated!
		/// </summary>
		public Hearts()
		{
			for (int i = 0; i < 4; ++i)
			{
				players[i] = new HeartsPlayer();
			}

			deck = new CardDeck();
		}

		/// <summary>
		/// Awesome constructor.
		/// </summary>
		/// <param name="users">List of users</param>
		public Hearts(List<User> users)
		{
			if (users.Count <= 4)
			{
				for (int i = 0; i < users.Count; ++i)
				{
					players[i].user = users[i];
				}
				for (int i = users.Count ; i < 4; ++i)
				{
					players[i].user = new User();
					players[i].user.UserName = "Computer";
				}
			}
			else
			{
				throw new TooManyPlayersException("There are too many players attempting to play Hearts.");
			}
		}

		/// <summary>
		/// Creates a single player instance of "Hearts"
		/// </summary>
		/// <param name="player">The user that wishes to play</param>
		public Hearts(User player) : this()
		{
			players[0].user = player;
			players[1].user = new User();
			players[1].user.UserName = "Computer";
			players[2].user = players[1].user;
			players[3].user = players[1].user;
		}

		/// <summary>
		/// Creates a two-player instance of "Hearts"
		/// </summary>
		/// <param name="user1">The game creator</param>
		/// <param name="user2">Second user</param>
		public Hearts(User user1, User user2) : this()
		{
			players[0].user = user1;
			players[1].user = user2;
			players[2].user = new User();
			players[2].user.UserName = "Computer";
			players[3].user = players[2].user;
		}

		/// <summary>
		/// Creates a three player instance of "Hearts"
		/// </summary>
		/// <param name="user1">The game creator</param>
		/// <param name="user2">Second user</param>
		/// <param name="user3">Third user</param>
		public Hearts(User user1, User user2, User user3) : this()
		{
			players[0].user = user1;
			players[1].user = user2;
			players[2].user = user3;
			players[3].user = new User();
			players[3].user.UserName = "Computer";
		}

		/// <summary>
		/// Creates a four player instance of "Hearts"
		/// </summary>
		/// <param name="user1">The game creator</param>
		/// <param name="user2">Second user</param>
		/// <param name="user3">Third user</param>
		/// <param name="user4">Fourth user</param>
		public Hearts(User user1, User user2, User user3, User user4) : this()
		{
			players[0].user = user1;
			players[1].user = user2;
			players[2].user = user3;
			players[3].user = user4;
		}

		/// <summary>
		/// Gives each player a freshly dealt hand of 13 cards.
		/// </summary>
		public void Deal()
		{
			deck.Shuffle();
			for (int i = 0; i < 4; ++i)
			{
				players[i].hand = (HeartsHand)deck.GetCards(13);
			}
		}

		/// <summary>
		/// Finds the player with the two of clubs
		/// </summary>
		/// <returns>The player with the Two of clubs</returns>
		private HeartsPlayer GetStartingPlayer()
		{
			Card match = new Card(2, Suit.Club);

			for (int i = 0; i < 4; ++i)
			{
				if (players[i].hand.Exists(x => x == match)) 
					return players[i];
			}

			return null;
		}

		/// <summary>
		/// Find the player that should start the round.
		/// </summary>
		/// <param name="winner"></param>
		/// <returns>The last game's winner or the player who has the two of clubs</returns>
		public HeartsPlayer GetStartingPlayer(HeartsPlayer winner)
		{
			if (winner == null)
				return GetStartingPlayer();

			return winner;
		}
	}
}