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

		public Board GetBoardById(int id)
		{
			return (from board in rep
					where board.id == id
					select board).SingleOrDefault();
		}

		public IQueryable<Board> GetBoards()
		{
			return rep.AsQueryable();
		}

		public IQueryable<Board> GetBoards(Game type)
		{
			var result = from n in rep
						 where n.Game == type
						 select n;
			return result.AsQueryable();
		}

		public IQueryable<User> GetBoardUsers(Board board)
		{
			// dummy
			return null;
		}


		public User GetBoardOwner(Board board)
		{
			// dummy
			return null;
		}
	}
}
