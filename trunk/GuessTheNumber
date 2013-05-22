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

	public class GuessTheNumber : IGame
	{

		private static Random randomGenerator;

		/// <summary>
		/// Returns a random integer in the specified range
		/// </summary>
		private static int randInt(int from, int to)
		{
			if (randomGenerator == null)
			{
				randomGenerator = new Random();
			}

			return randomGenerator.Next(from, to);
		}

		private GuessTheNumberPlayer[] players;
		private int turn;
		private int theNumber;
		private int from;
		private int to;
		private bool correct;

		public GuessTheNumber()
		{
			players = new GuessTheNumberPlayer[4];
			theNumber = randInt(0, 100); 
			turn = 0;
			from = 0;
			to = 100;
			correct = false;
		}

		/// <summary>
		/// Makes it the next player's turn
		/// </summary>
		private void IncrementTurn()
		{
			turn = (turn + 1) % 4;
		}

		/// <summary>
		///	Returns the index of the player or a negative number if he is not a player on the board
		/// </summary>
		private int GetPlayerIndex(GuessTheNumberPlayer player)
		{
			for (int i = 0; i < 4; ++i)
			{
				if (player.Equals(players[i]))
				{
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// Returns the player object for a specific username or null if there is no player on the board with the name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private GuessTheNumberPlayer GetPlayerByName(string name)
		{
			for (int i = 0; i < 4; ++i)
			{
				if (players[i].userName == name)
				{
					return players[i];
				}
			}

			return null;
		}

		/// <summary>
		/// Checks if a player has won and then lets all the computer players guess until it is a human players turn
		/// </summary>
		private int ActionCompleted()
		{

			// if last guess was correct
			if (correct)
			{
				return FinishMatch();
			}

			// let computer players make random guesses
			while (players[turn].IsAI())
			{
				// if (true)
				if (MakeGuess(randInt(from, to)))
				{
					// if the computer guessed correctly
					if (correct)
					{
						return FinishMatch();
					}
				}
			}

			// refresh once it is a humans turn
			return 1;
		}

		/// <summary>
		/// Restarts board so another match can be played on it.
		/// Also increments the amout of wins of the player whose turn it is.
		/// </summary>
		private int FinishMatch()
		{
			// assign points to winner, the one whose turn it is when game is over
			players[turn].wins += 1;

			// restart board so new game can start
			theNumber = randInt(0, 100);
			from = 0;
			to = 100;
			correct = false;

			// possibly let computers play again
			return ActionCompleted();
		}

		/// <summary>
		/// Makes a guess and returns true if it is a legal guess.
		/// </summary>
		private bool MakeGuess(int guess)
		{
			// if guess is out of range
			if (guess < from || guess > to)
			{
				return false;
			}

			// if guess is valid; update range
			if (guess >= from && guess < theNumber)
			{
				// player guessed too low
				from = guess + 1;
			}
			else if (guess <= to && guess > theNumber)
			{
				// player guessed too high
				to = guess - 1;
			}
			else
			{
				// the guess is correct; return true and don't increment turn
				correct = true;
				return true;
			}

			// increment turn before returning for the incorrect but valid guesses
			IncrementTurn();
			return true;
		}
























		/*
		 * IGame interface implementations for GuessTheNumber
		 */

		public void AddPlayers(List<User> players)
		{
			// assume there are players
			int i = 0;
			int k = 0;
			foreach (var player in players)
			{
				this.players[i++] = new GuessTheNumberPlayer(player.UserName, 0);
			}
			for (; i < 4; i++)
			{
				// TODO: the logic of naming computer players shouldn't be here
				this.players[i] = new GuessTheNumberPlayer("Computer" + k, 0);
			}
		}

		public List<User> GetPlayers()
		{
			List<User> playerList = new List<User>();

			for (int i = 0; i < 4; ++i)
			{
				// TODO: this wrapping is so redundant... interface needs to be re-thought
				//       the only thing this is used for is to get usernames
				playerList.Add(new User { UserName = players[i].userName });
			}

			return playerList;
		}

		/// <summary>
		/// Format of status string:
		/// [The number to be guessed]|[From]|[To]|[Whose turn it is]|[Username 1, Wins]|[Username 2, Wins]|[Username 3, Wins]|[Username 4, Wins]
		/// </summary>
		public string GetStatus()
		{
			var stringBuilder = new System.Text.StringBuilder();

			stringBuilder.Append(theNumber);
			stringBuilder.Append('|');
			stringBuilder.Append(from);
			stringBuilder.Append('|');
			stringBuilder.Append(to);
			stringBuilder.Append('|');
			stringBuilder.Append(players[turn].userName);
			foreach (GuessTheNumberPlayer player in players)
			{
				stringBuilder.Append('|');
				stringBuilder.Append(player.userName);
				stringBuilder.Append(',');
				stringBuilder.Append(player.wins);
			}

			return stringBuilder.ToString();
		}

		public void SetStatus(string status)
		{
			string[] s = status.Split('|');

			Int32.TryParse(s[0], out theNumber);
			Int32.TryParse(s[1], out from);
			Int32.TryParse(s[2], out to);

			int wins;
			for (int i = 0; i < 4; i++)
			{
				string[] playerString = s[4 + i].Split(',');
				Int32.TryParse(playerString[1], out wins);
				GuessTheNumberPlayer player = new GuessTheNumberPlayer(playerString[0], wins);
				players[i] = player;
				// if the current player is the one whose turn it is
				if (player.userName == s[3])
				{
					turn = i;
				}
			}
		}

		/// <summary>
		/// Tries to make a guess for the player/sender
		/// </summary>
		/// <returns>0 if no success, 1 if refresh is needed, 2 if game is over</returns>
		/// <remarks>
		/// It has been suggested that this function never returns 2 and needs to be reworked to return a bool
		/// </remarks>
		public int TryAction(string action, string sender)
		{
			// check if it is the senders turn
			if (players[turn].userName != sender)
			{
				// do nothing if it isn't
				return 0;
			}

			// try making the guess
			int guess;
			if (Int32.TryParse(action, out guess) && MakeGuess(guess))
			{
				return ActionCompleted();
			}
			else if (action.Contains("checkAI"))
			{
				return ActionCompleted();
			}
			else
			{
				// do nothing - just refresh
				return 1;
			}

		}  // public int TryAction(string action, string sender)

		public string GetGameOver()
		{
			return players[turn].userName;
		}

























		/*
		 * Nested classes of GuessTheNumber
		 */

		public class GuessTheNumberPlayer
		{

			public string userName = "Computer";
			public int wins = 0;

			public GuessTheNumberPlayer()
			{
				// intentionally empty
			}

			public GuessTheNumberPlayer(int wins)
			{
				this.wins = wins;
			}

			public GuessTheNumberPlayer(string userName, int wins)
			{
				this.userName = userName;
				this.wins = wins;
			}

			public bool IsAI()
			{
				return userName.StartsWith("Computer");
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}

				var p = obj as GuessTheNumberPlayer;

				return (this.userName == p.userName);
			}

			public override int GetHashCode()
			{
				return userName.GetHashCode();
			}

		}  // public class GuessTheNumberPlayer

	}  // public class GuessTheNumber : IGame

}  // namespace Windshield.Models.Games