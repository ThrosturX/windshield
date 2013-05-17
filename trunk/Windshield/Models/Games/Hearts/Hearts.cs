using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Windshield.Common;
using Windshield.Models;
using Windshield.Models.Common.Exceptions;

namespace Windshield.Models.Games
{
	public class Hearts : IGame
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
		public bool mustPlayTwoOfClubs = true;

		/// <summary>
		/// Default constructor. Note that instances require players to be instantiated!
		/// </summary>
		/// <remarks>Shouldn't be used.</remarks>
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
			AssignPlayerSlots(users);
			deck.Shuffle();
			DealTheCards();
			SortTheCardsOnTheHands();
			// Find the player with 2 of Clubs card
			turn = GetStartingPlayer(null);
		}

		/// <summary>
		/// Helper function for Hearts constructor. Assigns player slots to human players first, then computer players.
		/// </summary>
		/// <param name="users"></param>
		public void AssignPlayerSlots(List<User> users)
		{
			if (users.Count <= 4)
			{
				for (int i = 0; i < users.Count; ++i)
				{
					players[i].user = users[i];
				}
				for (int i = users.Count; i < 4; ++i)
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
		/// Gives each player a freshly dealt hand of 13 cards.
		/// </summary>
		public void DealTheCards()
		{
			for (int i = 0; i < 4; ++i)
			{
				players[i].hand = new Hand(deck.GetCards(13));
			}
		}

		/// <summary>
		/// Sorts the cards on the hands after the cards have been dealt to the players
		/// Returns sorted hands for all the players
		/// </summary>
		public void SortTheCardsOnTheHands()
		{
			foreach (var player in players)
			{
				// cardValuePair holds a pair ([card index in hand], [card value])
				List<Pair> cardValuePair = new List<Pair>();
				// Get card value for each card and place the pair in the list
				foreach (var card in player.hand)
				{
					cardValuePair.Add(new Pair(card.CardValue(),card));
				}
				// Sort
				cardValuePair = cardValuePair.OrderBy(x => x.First).ToList();
				// Add the cards in the correct order from the sorted list
				List<Card> newCardList = new List<Card>();
				foreach (Pair cardValue in cardValuePair)
				{
					newCardList.Add((Card)cardValue.Second);
				}
				// Create the new sorted hand
				player.hand = new Hand(newCardList);
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
					{
						return players[i];
					}
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
			{
				return false;
			}

			// basic sanity check: make sure the player actually has this card
			if (player.hand.FindCard(card.face, card.suit) == null)
			{
				return false;
			}

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
				if (mustPlayTwoOfClubs)
				{
					if (card.face != 2 || card.suit != Suit.Club)
					{
						return false;
					}

					mustPlayTwoOfClubs = false;
				}
				trick = new Trick(player, card);
				trickOngoing = true;
			}

			// remove the card from the hand
			player.hand.RemoveCard(card);


			IncrementTurn();
			return true;
		}

		public int GetPlayerIndex(HPlayer player)
		{
			for (int i = 0; i < 4; ++i)
			{
				if (player == players[i])
				{
					return i;
				}
			}

			return -1;
		}

		public void SetStatus(string status)
		{
			// split the string
			string[] splitStatus  = status.Split('|');
			// trick
			string[] tInfo = splitStatus[0].Split(',');
			// hands
			string[] hInfo = splitStatus[1].Split('/');
			// stats
			string[] sInfo = splitStatus[2].Split('/');

			// is there an ongoing trick?
			if (tInfo[0] == " ")
			{
				trickOngoing = false;
			}
			else
			{
				trickOngoing = true;
			}
			
			// find cards in trick
			if (trickOngoing)
			{
				trick = new Trick();

				// get the leading suit
				switch (tInfo[0][0])
				{
					case 'H':
						{
							trick.leader = Suit.Heart;
							break;
						}
					case 'S':
						{
							trick.leader = Suit.Spade;
							break;
						}
					case 'D':
						{
							trick.leader = Suit.Diamond;
							break;
						}
					case 'C':
						{
							trick.leader = Suit.Club;
							break;
						}
					default:
						{
							trick.leader = Suit.Joker; // shouldn't happen
							break;
						}
				}

				// get all the cards
				for (int i = 1; i <= 4; ++i)
				{
					string[] tCards = tInfo[i].Split('-');

					Card card = Card.SpawnCardFromString(tCards[0]);
					HPlayer player = GetPlayerByName(tCards[1]);

					if (card.suit != Suit.Joker && player != null)
					{
						trick.AddCard(player, card);
					}
				}

			}

			// set the correct turn
			int index;
			int.TryParse(tInfo[5], out index);
			turn = players[index];


			// find the player's hands
			for (int i = 0; i < 4; ++i)
			{
				players[i].hand.Clear();
				string[] handCards = hInfo[i].Split(',');
				for (int j = 0; j < 13; ++j)
				{
					Card card;
					try
					{
						card = Card.SpawnCardFromString(handCards[j]);
					}
					catch (Exception)
					{
						continue;
					}

					if (!card.IsJoker())
					{
						players[i].hand.Add(card);
					}
				}
			}

			for (int i=0; i<4; ++i)
			{
				string[] playerStats = sInfo[i].Split(',');
				int thePoints = 0;
				int.TryParse(playerStats[0], out thePoints);
				players[i].matchPoints = thePoints;
				int.TryParse(playerStats[1], out thePoints);
				players[i].gamePoints = thePoints;
				// finally, check if two of clubs must be played...
				if (players[i].gamePoints > 0 || players[i].matchPoints > 0)
				{
					mustPlayTwoOfClubs = false;
				}
				if (players[i].hand.Count < 13)
				{
					mustPlayTwoOfClubs = false;
				}
			}

		}

		private HPlayer GetPlayerByName(string name)
		{
			for (int i = 0; i < 4; ++i)
			{
				if (players[i].user.UserName == name)
				{
					return players[i];
				}
			}

			return null;
		}

		/// <summary>
		/// Creates a status string describing the game's state.
		/// </summary>
		/// <returns>The game's state in string format</returns>
		/// <remarks>
		/// Format:
		/// [Trick Info]|[Players' Cards]|[Players' stats]
		/// Trick info example:
		/// [leader,card-playername,card-playername,card-playername,card-playername,turn]
		/// where turn is the playername of the player whose turn it is. 
		/// 
		/// Players' cards format:
		/// [card,card,card,.../...]
		/// 
		/// Players' stats example:
		/// [13,20,john/0,0,steven/3,5,banana/7,40,alex42]
		/// </remarks>
		public string GetStatus()
		{
			StringBuilder builder = new StringBuilder();
			// Cards in trick
			if (trickOngoing)
			{
				// leading suit
				switch (trick.leader)
				{
					case Suit.Heart:
						{
							builder.Append("H,");
							break;
						}
					case Suit.Spade:
						{
							builder.Append("S,");
							break;
						}
					case Suit.Diamond:
						{
							builder.Append("D,");
							break;
						}
					case Suit.Club:
						{
							builder.Append("C,");
							break;
						}
					default:
						{
							builder.Append(" ,");
							break;
						}

				}

				// cards-username in trick
				for (int i = 0; i < 4; ++i)
				{
					Card card = trick.GetCardAtIndex(i);

					if (card.IsJoker())
					{
						builder.Append("  - ");
					}
					else
					{
						builder.Append(Card.CreateCardString(card));
					}
					builder.Append("-");
					builder.Append(trick.GetPlayerNameAtIndex(i));
					builder.Append(",");
				}

			}
			else
			{
				builder.Append(" ,  - ,  - ,  - ,  - ,");
			}
			
			// Player whose turn it is (number)
			builder.Append(GetPlayerIndex(turn));

			builder.Append('|');

			// Players' cards

			for (int i = 0; i < 4; ++i)
			{
				builder.Append(players[i].hand.CreateHandString());
				if (i != 3)
					builder.Append('/');
			}

			builder.Append('|');

			// Players' stats

			for (int i = 0; i < 4; ++i)
			{
				builder.Append(players[i].matchPoints.ToString() + ",");
				builder.Append(players[i].gamePoints.ToString() + ",");
				builder.Append(players[i].user.UserName);
				if (i != 3)
					builder.Append('/');
			}

			return builder.ToString();
		}

		public int TryAction(string action, string sender)
		{
			// check the sender
			HPlayer player = new HPlayer();

			foreach (var p in players)
			{
				if (p.user.UserName == sender)
				{
					player = p;
					break;
				}
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
					switch (cardString[2])
					{
						case "H":
							{
								card.suit = Suit.Heart;
								break;
							}
						case "S":
							{
								card.suit = Suit.Spade;
								break;
							}
						case "D":
							{
								card.suit = Suit.Diamond;
								break;
							}
						case "C":
							{
								card.suit = Suit.Club;
								break;
							}
						default:
							return 0;	// invalid card
					}
					
					// get rank
					int rank;
					if (int.TryParse(cardString[1], out rank))
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
						switch (cardString[1][0])
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
					ActionCompleted();
					return 1;	// success!
				}
			}
			else if (action.Contains("refresh"))
			{
				return 1; // do nothing, but refresh
			}
			else if (action.Contains("checkAI"))
			{
				return ActionCompleted();
			}

			// no success
			return 1; // refresh
		}  //public bool PlayCard(HPlayer player, Card card)

		public int ActionCompleted()
		{
			// check if the match is over
			HPlayer winner = CheckWinner();
			int played = 0;

			// if the match is over
			if (winner != null)
			{
				return FinishMatch(winner);
			}
			
			// it's not over, let the computer play!
			while (turn.user.UserName.StartsWith("Computer"))
			{
				// if the match is over
				if (winner != null)
				{
					return FinishMatch(winner);
				}
				PlayAI();
				played = 1;
			}

			return played;
		}

		private int PlayAI()
		{
			Card card;

			// Just try playing a random card until a valid card is played
			while (true)
			{
				card = turn.hand.RandomCard();
				if (card != null)
				{
					if (PlayCard(turn, card))
					{
						return 1;
					}
				}
				else
				{
					IncrementTurn();
					return 1;
				}
			}
		}

		private int FinishMatch(HPlayer winner)
		{
			// assign points to everyone
			if (winner.matchPoints == 26)
			{
				foreach (var p in players)
				{
					if (p != winner)
					{
						p.gamePoints += 26;
					}
				}
			}
			else
			{
				foreach (var p in players)
				{
					p.gamePoints += p.matchPoints;
				}
			}

			// make the winner start the next round
			turn = winner;
			
			// other cleanup
			trickOngoing = false;

			foreach (var p in players)
			{
				p.matchPoints = 0;
				if (p.gamePoints >= 100)
				{
					return FinishGame(winner);
				}
			}

			deck.Shuffle();
			DealTheCards();
			
			ActionCompleted();

			return 1;
		}

		private int FinishGame(HPlayer winner)
		{
			throw new NotImplementedException();
		}

		private HPlayer CheckWinner()
		{
			foreach (var p in players)
			{
				if (p.hand.Count != 0)
				{
					return null; // the game is not over!
				}
			}

			// find who wins the match

			HPlayer winner = new HPlayer();
			winner.gamePoints = 27;

			foreach (var p in players)
			{
				// make sure he's not winning completely!
				if (p.gamePoints == 26)
				{
					return p;
				}

				if (winner == null)
				{
					winner = p;
				}
				else if (p.gamePoints < winner.gamePoints)
				{
					winner = p;
				}
			}

			return winner;
		}






		// TODO: Implement dummy interface functions!!!


		public void AddPlayers(List<User> players)
		{
			AssignPlayerSlots(players);
			deck.Shuffle();
			DealTheCards();
			SortTheCardsOnTheHands();
			// Find the player with 2 of Clubs card
			turn = GetStartingPlayer(null);
		}

		public List<User> GetPlayers()
		{
			List<User> pList = new List<User>();

			for (int i=0; i<4; ++i)
			{
				pList.Add(players[i].user);
			}

			return pList;
		}

		
		public string GetGameOver()
		{
			return "DERP";
		}


	}
}