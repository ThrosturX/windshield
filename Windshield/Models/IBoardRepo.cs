using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IBoardRepo
	{
		void AddBoard(Board board);
		void Save();
		void DeleteBoard(Board board);
		Board GetBoardById(int id);
		IQueryable<Board> GetBoards();
		IQueryable<Board> GetBoards(Game game);
		IQueryable<User> GetBoardUsers(Board board);
		User GetBoardOwner(Board board);
	}
}