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

		public Game GetGameByName(string name)
		{
			var result = from game in db.Games
						 where game.name.Equals(name)
						 select game;
			return result.SingleOrDefault();
		}

		public Game GetGameByID(int id)
		{
			var result = from game in db.Games
						 where game.id.Equals(id)
						 select game;
			return result.SingleOrDefault();
		}

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
				   orderby gameRatings.rating descending
				   select gameRatings;
		}
		public List<StatisticsViewModel> GetTopRatingsForViewModel(Game game)
		{

			if (game == null)
				return null;

			List<StatisticsViewModel> statistics = new List<StatisticsViewModel>();

			var ratings = (from gameRatings in db.GameRatings
						   where gameRatings.idGame == game.id
						   orderby gameRatings.rating descending
						   select gameRatings);
			
			if (!(ratings.Any()))
				return null;

			foreach (GameRating r in ratings)
			{
				StatisticsViewModel m = new StatisticsViewModel();
				m.Name = r.User.UserName;
				m.Rating = r.rating;
				m.TimesPlayed = r.timesPlayed;
				statistics.Add(m);
			}
			return statistics;
		}
	}
}