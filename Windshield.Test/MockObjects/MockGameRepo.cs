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

	}
}
