using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;
using Windshield.Models.Games;

namespace Windshield.ViewModels
{
	public class BoardViewModel
	{
		public int id;  // board id
		public string viewName;  // the View name
		public string gameName;  // the Game name
		public string status;
		private List<User> players = new List<User>();

		public BoardViewModel(Board board)
		{
			id = board.id;
			viewName = board.Game.model;
			gameName = board.Game.name;
			status = board.status;
		}

		public void AddPlayer(User player)
		{
			players.Add(player);
		}

		public void AddPlayers(List<User> players)
		{
			this.players.AddRange(players);
		}

		public List<User> GetPlayers(int n)
		{
			int i = players.Count;
			List<User> playerList = new List<User>(players);
			for (int j = 1; j <= n - i; j++)
			{
				playerList.Add(new User{ UserName = "Computer" + j });
			}
			return playerList;
		}

		public string[] GetPlayerNames(int n)
		{
			int c = players.Count;
			int b = 0;

			string playerString = "";

			foreach (var p in players)
			{
				playerString += p.UserName + ",";
				b++;
			}

			for (int i = b; i <= n; i++)
			{
				playerString += "Computer" + (i - b + 1).ToString() + ","; 
			}

			return playerString.Split(',');
		}

		public string GetPlayerName(int n)
		{
			if (n < players.Count)
			{
				return players.ElementAt(n).UserName;
			}

			return "Computer" + (n + 1).ToString();
		}

	}
}