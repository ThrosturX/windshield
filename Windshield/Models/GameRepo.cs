using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;


namespace Windshield.Models
{
	public class GameRepo : IGameRepo
	{
		
		public static BoardGamesDataContext db = null;

		public GameRepo()
		{
			if (db == null)
			{
				db = new BoardGamesDataContext();
			}
		}

		//
		// insert methods

		public void AddGame(Game game)
		{
			db.Games.InsertOnSubmit(game);
		}

		//
		// delete methods

		public void DeleteGame(Game game)
		{
			db.Games.DeleteOnSubmit(game);
		}

		//
		// commit methods

		public void Save()
		{
			db.SubmitChanges();
		}

		//
		// query methods

		public IQueryable<Game> GetAllGames()
		{
			return db.Games;
		}

		public IQueryable<GameRating> GetTopRatings()
		{
			return from gameRatings in db.GameRatings
			       orderby gameRatings.rating descending
			       select gameRatings;
		}

		public IQueryable<GameRating> GetTopRatings(Game game)
		{
			return from gameRatings in db.GameRatings
				   where gameRatings.idGame == game.id
				   select gameRatings;
		}

	}
}