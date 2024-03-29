﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;
using Windshield.ViewModels;

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

		public void AddRating(GameRating rating)
		{
			db.GameRatings.InsertOnSubmit(rating);
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

		/// <summary>
		/// Gets the top games played for the index view specified by id
		/// if id is null, then we retrieve all games
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public List<PopularViewModel> GetTopGamesPlayedForViewModel(int? id)
		{
			IQueryable<Game> games;
			if (!(id.HasValue))
			{
				games = from game in db.Games
							select game;
			}
			else
			{
				games = (from game in db.Games
							 orderby game.timesPlayed descending
							 select game).Take(id.Value);
			}
			List<PopularViewModel> model = new List<PopularViewModel>();

			foreach(var g in games)
			{
				PopularViewModel m = new PopularViewModel();
				m.Name = g.name;
				m.Url = "/Home/GameDescription?name=" + g.name;
				model.Add(m);

				if (g.image == null)
					m.Image = "pug.jpg";
				else
					m.Image = g.image;
			}
			return model;							   
		}

		public List<PopularViewModel> GetNewGamesPlayedForViewModel()
		{
			var games = (from game in db.Games
						 orderby game.timesPlayed ascending
						 select game).Take(5);

			List<PopularViewModel> model = new List<PopularViewModel>();

			foreach (var g in games)
			{
				PopularViewModel m = new PopularViewModel();
				m.Name = g.name;
				m.Url = "/Home/GameDescription?name=" + g.name;
				if (g.image == null)
					m.Image = "pug.jpg";
				else
					m.Image = g.image;
				model.Add(m);
			}

			return model;
		}

		public GameRating GetGameRatingByNameAndGameID(string name, int id)
		{

			var result = (from g in GameRepo.db.GameRatings
						  where g.userName == name && g.idGame == id
						  select g).SingleOrDefault();

			return result;
		}
	}
}