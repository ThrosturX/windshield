using System;
using System.Collections.Generic;
using System.Web;

namespace Windshield.Models.Games.Common.Ratings
{
	public class Elo
	{
		public int points { get; set; }
		public int gameID { get; set; }
		public User user { get; set; }
		public int K { get; set; }

		/// <summary>
		/// Default constructor -- creates an Elo at a default value.
		/// </summary>
		/// <remarks>
		/// NOTE: User and GameID must be initialized if this constructor is used
		/// </remarks>
		public Elo()
		{
			points = 1200;
			K = 10;
		}

		public Elo(int id, User usr) : this()
		{
			gameID = id;
			user = usr;
		}

		/// <summary>
		///  Updates a single player's Elo
		/// </summary>
		/// <param name="score">The player's score in the game (i.e. 1 is a complete victory, 0 is a loss)</param>
		/// <param name="otherPlayer">The other player's Elo points</param>
		public void Update(float score, int otherPoints)
		{
			points = (int)(points + K * (score - (1 / (1 + 10 * (otherPoints - points) / 400))));
		}

		/// <summary>
		/// Updates all players' Elo scores in a particular game.
		/// </summary>
		/// <param name="score">The score for the player who is being updated (usually the winner, or outlier)</param>
		/// <param name="otherPlayers">List of Elo objects, one for each other player</param>
		public void UpdateAll(float score, List<Elo> otherPlayers)
		{
			int count = 0;
			int accumulator = 0;
			int otherPoints = 1200;
			foreach (var rating in otherPlayers)
			{
				accumulator += rating.points;
				count++;
				rating.Update(1 - score, points);
			}

			if (count == 0)
			{
				otherPoints = 100; // computer
			}
			else
			{
				otherPoints = accumulator / count;
			}

			this.Update(score, otherPoints);
		}
	}
}