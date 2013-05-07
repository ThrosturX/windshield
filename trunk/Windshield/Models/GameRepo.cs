using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;


namespace Windshield.Models
{
	public class GameRepo : IGameRepo
	{

		public static BoardGamesDataContext db = new BoardGamesDataContext();

		// making GameRepo a singleton class
		private static GameRepo instance = null;
		public static GameRepo Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new GameRepo();
				}
				return instance;
			}
		}

		//
		// insert methods

		public void AddGame(Game game)
		{ 
		}

		//
		// delete methods

		public void DeleteGame(Game type)
		{ 
		
		}

		//
		// commit methods

		//
		// query methods

		public IQueryable<Game> GetAllGames()
		{
			return db.Games;
		}

		public IQueryable<User> GetTopPlayers()
		{
			var result = from gameRatings in db.GameRatings
						 orderby gameRatings.rating descending
						 select gameRatings;
			return null;
		}

		public IQueryable<User> GetTopPlayersByGame(Game type)
		{
			return null;
		}

	}
}