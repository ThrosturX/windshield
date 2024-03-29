﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.ViewModels;

namespace Windshield.Models
{
	public interface IGameRepo
	{
		//
		// insert methods

		void AddGame(Game type);
		void AddRating(GameRating rating);

		//
		// delete methods

		void DeleteGame(Game type);

		//
		// commit methods

		void Save();

		//
		// query methods

		Game GetGameByName(string name);
		Game GetGameByID(int id);
		GameRating GetGameRatingByNameAndGameID(string name, int id);

		List<StatisticsViewModel> GetTopRatingsForViewModel(Game game);
		List<PopularViewModel> GetTopGamesPlayedForViewModel(int ?id);
		List<PopularViewModel> GetNewGamesPlayedForViewModel();
		
		IQueryable<Game> GetAllGames();
		IQueryable<GameRating> GetTopRatings();
		IQueryable<GameRating> GetTopRatings(Game game);
	
	}
}