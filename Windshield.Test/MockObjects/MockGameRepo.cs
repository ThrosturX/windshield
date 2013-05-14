using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windshield.Models;

namespace Windshield.Test.MockObjects
{
	class MockGameRepo : IGameRepo
	{
		public List<Game> rep = new List<Game>();

		public void AddGame(Game type)
		{
			rep.Add(type);
		}

		public void DeleteGame(Game type)
		{
			rep.Remove(type);
		}

		public Game GetGameByName(string name)
		{
			var result = from game in rep 
						 where game.name == name
						 select game;
			return result.SingleOrDefault();
		}

		public Game GetGameByID(int id)
		{
			return (from game in rep
					where game.id == id
					select game).SingleOrDefault();
		}

		public IQueryable<Game> GetAllGames()
		{
			return rep.AsQueryable();
		}

		public IQueryable<GameRating> GetTopRatings()
		{
			// dummy
			return null;
		}

		public IQueryable<GameRating> GetTopRatings(Game game)
		{
			// dummy
			return null;
		}

		public IQueryable<User> GetTopPlayersByGame(Game game)
		{
			// dummy
			return null;
		}
		public List<StatisticsViewModel> GetTopRatingsForViewModel(Game game)
		{
			// dummy
			return null;
		}
	}
}
