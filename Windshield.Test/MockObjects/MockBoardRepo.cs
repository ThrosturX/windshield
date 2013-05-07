using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windshield.Models;

namespace Windshield.Test.MockObjects
{
	// TODO!!
	internal class MockBoardRepo : IBoardRepo
	{
		public void AddBoard(Board board)
		{

		}

		public void DeleteBoard(Board board)
		{

		}

		public IEnumerable<Board> GetBoards()
		{
			return null;
		}

		public IEnumerable<Board> GetBoards(Game type)
		{
			return null;
		}

		public IEnumerable<User> GetBoardUsers(Board board)
		{
			return null;
		}

		public Game GetGameType(Board board)
		{
			return null;
		}

		public User GetBoardOwner(Board board)
		{
			return null;
		}
	}
}
