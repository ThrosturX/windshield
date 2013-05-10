﻿using System;
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
		public List<User> group;

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