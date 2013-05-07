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
		IQueryable<Game> GetAllGames();
		IQueryable<User> GetTopPlayers();
		IQueryable<User> GetTopPlayersByGame(Game type);
	}
}