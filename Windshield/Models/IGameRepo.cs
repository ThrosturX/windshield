using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.ViewModels;

namespace Windshield.Models
{
	public interface IGameRepo
	{
		
		void AddGame(Game type);
		void DeleteGame(Game type);
		Game GetGameByName(string name);
		Game GetGameByID(int id);
		List<StatisticsViewModel> GetTopRatingsForViewModel(Game game);
		List<PopularViewModel> GetTopGamesPlayedForViewModel(int ?id);
		List<PopularViewModel> GetNewGamesPlayedForViewModel();
		IQueryable<Game> GetAllGames();
		IQueryable<GameRating> GetTopRatings();
		IQueryable<GameRating> GetTopRatings(Game game);
	}
}