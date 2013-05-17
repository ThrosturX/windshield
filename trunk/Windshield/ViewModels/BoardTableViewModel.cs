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
			GameName = "All games";
			IsEmpty = true;
			Rows = new List<Row>();
		}

		public BoardTableViewModel(string gameName) : this()
		{
			GameName = gameName;
			IsEmpty = true;
			Rows = new List<Row>();
		}

		public void Add(Windshield.Models.Board board)
		{
			Rows.Add(new Row(board));
			if (IsEmpty)
			{
				IsEmpty = false;
			}
		}

		public void Add(IQueryable<Windshield.Models.Board> boards)
		{
			foreach (Windshield.Models.Board board in boards)
			{
				Rows.Add(new Row(board));
			}
			if (boards.Count() >= 1)
			{
				IsEmpty = false;
			}
		}

		public class Row
		{
			public int ID { get; set; }
			public int Players { get; set; }
			public int MaxPlayers { get; set; }
			public string GameName { get; set; }
			public string OwnerName { get; set; }
			
			public Row(Windshield.Models.Board board)
			{
				ID = board.id;
				Players = board.Players.Count();
				MaxPlayers = board.Game.maxPlayers;
				GameName = board.Game.name;
				OwnerName = board.ownerName;
			}
		}
	}
}