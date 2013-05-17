using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windshield.Models;
using Windshield.ViewModels;

namespace Windshield.Test.MockObjects
{
	class MockGameRepo : IGameRepo
	{
		public List<Game> rep = new List<Game>();

		public void AddGame(Game type)
		{
			rep.Add(type);
		}

		public void AddRating(GameRating rating)
		{
			throw new NotImplementedException();
		}

		public void DeleteGame(Game type)
		{
			rep.Remove(type);
		}

		public void Save()
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		public IQueryable<GameRating> GetTopRatings(Game game)
		{
			throw new NotImplementedException();
		}

		public IQueryable<User> GetTopPlayersByGame(Game game)
		{
			throw new NotImplementedException();
		}

		public List<StatisticsViewModel> GetTopRatingsForViewModel(Game game)
		{
			throw new NotImplementedException();
		}

		public List<PopularViewModel> GetTopGamesPlayedForViewModel(int? id)
		{
			throw new NotImplementedException();
		}

		public List<PopularViewModel> GetNewGamesPlayedForViewModel()
		{
			throw new NotImplementedException();
		}
	}
}
