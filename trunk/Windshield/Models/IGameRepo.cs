using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IGameRepo
	{
		void AddGame(Game type);
		void DeletGame(Game type);
		List<Game> GetAllGames();
		List<Game> GetGamesByType(Game type);
		List<User> GetTopPlayers();
		List<User> GetTopPlayersByGame(Game type);
	}
}