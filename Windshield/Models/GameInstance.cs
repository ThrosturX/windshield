using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public class GameInstance
	{
		public Board theGame;
		public string theView;
		public List<User> group;

		public GameInstance(Board gameBoard, string gameView)
		{
			theGame = gameBoard;
			theView = gameView;
		}

		public void addPlayer(User player)
		{
			group.Add(player);
		}
	}
}