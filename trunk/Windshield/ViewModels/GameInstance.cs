using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models.Games;

namespace Windshield.Models
{
	public class GameInstance
	{
		public int id;
		public AGame theGame;
		public string theView;
		public string theName;
		public List<User> group;

		public GameInstance(AGame gameBoard, Board board)
		{
			id = board.id;
			theGame = gameBoard;
			theView = board.Game.model;
			theName = board.Game.name;
		}

		public GameInstance(int ID, AGame gameBoard, string gameView)
		{
			id = ID;
			theGame = gameBoard;
			theView = gameView;
		}

		public void addPlayer(User player)
		{
			group.Add(player);
		}
	}
}