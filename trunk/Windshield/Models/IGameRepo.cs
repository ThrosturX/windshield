using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IGameRepo
	{
		void AddGame(Game type);
		void DeleteGame(Game type);
		Game GetGameByName(string name);
		IQueryable<Game> GetAllGames();
		IQueryable<GameRating> GetTopRatings();
		IQueryable<GameRating> GetTopRatings(Game game);
	}
}