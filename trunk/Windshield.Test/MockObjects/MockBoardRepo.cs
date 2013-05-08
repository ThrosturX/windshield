using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windshield.Models;

namespace Windshield.Test.MockObjects
{

	internal class MockBoardRepo : IBoardRepo
	{
		public List<Board> rep = new List<Board>();
	
		public void AddBoard(Board board)
		{
			rep.Add(board);
		}

		public void DeleteBoard(Board board)
		{
			rep.Remove(board);
		}

		public IEnumerable<Board> GetBoards()
		{
			return rep.AsEnumerable();
		}

		public IEnumerable<Board> GetBoards(Game type)
		{
			var result = from n in rep
						 where n.Game == type
						 select n;
			return result;
		}

		public IEnumerable<User> GetBoardUsers(Board board)
		{
			// dummy
			return null;
		}

		public Game GetGameType(Board board)
		{
			var result = from n in rep
						 where n.id == board.id
						 select n.Game;
			return result.SingleOrDefault();
		}

		public User GetBoardOwner(Board board)
		{
			// dummy
			return null;
		}
	}
}
