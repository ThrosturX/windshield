using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

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


	}
}