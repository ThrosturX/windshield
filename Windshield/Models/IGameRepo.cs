﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IGameRepo
	{
		void AddGame(GameType type);
		void DeletGame(GameType type);
		List<GameType> GetAllGames();
		List<GameType> GetGamesByType(GameType type);
		List<User> GetTopPlayers();
		List<User> GetTopPlayersByGame(GameType type);
	}
}