using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;


namespace Windshield.Models
{
	public class GameRepo : IGameRepo
	{
	
		static BoardGamesDataContext dBase = new BoardGamesDataContext();

		private static GameRepo gRep = null;

		public static GameRepo getgRep()
		{
			if (gRep == null)
			{
				gRep = new GameRepo();
			}
			return gRep;
		}

		//
		// query methods





		public void AddGame(Game type)
		{ 
			
		}

		public void DeletGame(Game type)
		{ 
		
		}

		public IQueryable<Game> GetAllGames()
		{
			return dBase.Games;
		}

		public IQueryable<User> GetTopPlayers()
		{
			return null;
		}

		public IQueryable<User> GetTopPlayersByGame(Game type)
		{
			return null;
		}
	}
}