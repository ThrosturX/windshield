using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Common;
using Windshield.Models;
using Windshield.Models.Common.Exceptions;
using Windshield.Models.Games.Hearts;

namespace Windshield.Models.Games.Hearts
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

		public HPlayer [] players;
		public HPlayer turn;
		public CardDeck deck;
		public Trick trick;
		public Board board;
		public bool trickOngoing = false;
		public bool brokenHearts;

		/// <summary>
		/// Default constructor. Note that instances require players to be instantiated!
		/// </summary>
		public Hearts()
		{
			board = new Board();
			board.status = "";

			players = new HPlayer[4];

			for (int i = 0; i < 4; ++i)
			{
				players[i] = new HPlayer();
			}

			deck = new CardDeck();
			brokenHearts = false;
		}

		/// <summary>
		/// Awesome constructor.
		/// </summary>
		/// <param name="users">List of users</param>
		public Hearts(List<User> users) : this()
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

			// Deal cards to the players
			Deal();
			turn = GetStartingPlayer(null);
		}

		/// <summary>
		/// Creates a single player instance of "Hearts"
		/// </summary>
		/// <param name="player">The user that wishes to play</param>
		public Hearts(User player)
			: this()
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
				players[i].hand = new Hand(deck.GetCards(13));
			}
		}

		/// <summary>
		/// Makes it the next player's turn
		/// </summary>
		public void IncrementTurn()
		{
			for (int i = 0; i < 4; ++i)
			{
				if (turn == players[i])
				{
					turn = players[(++i % 4)];
				}
			}
		}

		/// <summary>
		/// Finds the player with the two of clubs
		/// </summary>
		/// <returns>The player with the Two of clubs</returns>
		private HPlayer GetStartingPlayer()
		{
			for (int i = 0; i < 4; ++i)
			{
				foreach (var card in players[i].hand)
				{
					if (card.suit == Suit.Club && card.face == 2)
						return players[i];
				}
			}

			return null;
		}

		/// <summary>
		/// Find the player that should start the round.
		/// </summary>
		/// <param name="winner"></param>
		/// <returns>The last game's winner or the player who has the two of clubs</returns>
		public HPlayer GetStartingPlayer(HPlayer winner)
		{
			if (winner == null)
			{
				return GetStartingPlayer();
			}

			return winner;
		}

		public bool PlayCard(HPlayer player, Card card)
		{
			if (player != turn)
				return false;

			if (trickOngoing)
			{
				// check if the card he's playing is valid
				if (card.suit != trick.leader)
				{
					// make sure he doesn't have a card in the leading suit
					foreach (var found in player.hand)
					{
						if (found.suit == trick.leader)
						{
							return false;
						}
					}

					// check if he's playing hearts, and only allow him to play it if hearts are broken
					// or if he has no other choice
					if (card.suit == Suit.Heart && trick.leader != Suit.Heart)
					{
						// check if hearts are broken
						if (brokenHearts == false)
						{
							// check if he only has hearts
							foreach (var found in player.hand)
							{
								if (found.suit != Suit.Heart)
								{
									return false;
								}
							}
						}
					}
				}

				// add the card to the trick
				trick.Add(new KeyValuePair<HPlayer, Card>(player, card));

				// check if this is the last card for this trick
				if (trick.Count == 4)
				{
					// allocate points to the claimer and clear the trick
					KeyValuePair<HPlayer, int> allocation = trick.GetClaimer();

					allocation.Key.matchPoints += allocation.Value;

					trickOngoing = false;
					trick.RemoveAll(x => true);
				}

			}
			else
			{
				trick = new Trick(player, card);
				trickOngoing = true;
			}

			IncrementTurn();
			return true;
		}
		
		public int TryAction(string action, string sender)
		{
			// check the sender
			HPlayer player = new HPlayer();

			foreach (var p in players)
			{
				if (p.user.UserName == sender)
					player = p;
			}
			
			// not a valid sender
			if (player == null)
				return 0;

			// examples
			// play|2|S (try to play the two of spades)
			// play|A|H (ace of hearts)
			if (action.Contains("play"))
			{
				// check if it's the player's turn
				if (turn != player)
				{
					return 0;
				}

				// find which card is being played
				// H = Hearts, S = Spades, D = Diamons, C = Clubs
				string[] cardString = action.Split('|');

				Card card = new Card();

				try
				{
					// get suit
					switch (cardString[1][0])
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
							return 0;	// invalid card
					}
					
					// get rank
					int rank;
					if (int.TryParse(cardString[2], out rank))
					{
						if (rank > 0 && rank < 13)
						{
							card.face = rank;
						}
						else
						{
							return 0; // invalid rank
						}
					}
					else
					{
						switch (cardString[2][0])
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
								return 0; // invalid card
						}
					}
				}
				catch (Exception)
				{
					return 0; // fail; malformed action
				}

				// try playing the card
				if (PlayCard(player, card))
				{
					return 1;	// success!
				}
			}

			// no success
			return 0;
		}
		//public bool PlayCard(HPlayer player, Card card)
	}
}