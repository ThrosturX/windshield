using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IBoardRepo
	{
		void AddBoard(Board board);	
		void DeleteBoard(Board board);
		void Save();
		Board GetBoardById(int id);
		IQueryable<Board> GetBoards();
		IQueryable<Board> GetBoards(Game game);
		IQueryable<Board> GetBoards(User user);
		IQueryable<Board> GetBoards(Game game, User user);
		IQueryable<User> GetBoardUsers(Board board);
		User GetBoardOwner(Board board);
	}
}