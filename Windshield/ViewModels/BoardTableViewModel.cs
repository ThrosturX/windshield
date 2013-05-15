using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.ViewModels
{
	/// <summary>
	/// A ViewModel
	/// </summary>
	public class BoardTableViewModel
	{
		// EDIT: 15 - 12:30 - Ragnar

		public string GameName { get; set; }
		public List<Row> Rows { get; set; }
		public bool IsEmpty { get; set; }

		public BoardTableViewModel()
		{
			GameName = "All Games";
			IsEmpty = true;
		}

		public BoardTableViewModel(string gameName)
		{
			GameName = gameName;
			IsEmpty = true;
		}
		
		public void Add(Windshield.Models.Board board)
		{
			Rows.Add(new Row(board));
			IsEmpty = false;
		}

		public class Row
		{
			public int ID { get; set; }
			public int MaxPlayers { get; set; }
			public string OwnerName { get; set; }
			public int Players { get; set; }
		
			public Row(Windshield.Models.Board board)
			{
				ID = board.id;
				MaxPlayers = board.Game.maxPlayers;
				OwnerName = board.ownerName;
				Players = board.Players.Count();
			}
		}
	}
}