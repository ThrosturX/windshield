using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IBoardRepo
	{

		//
		// insert methods

		void AddBoard(Board board);
		void AddPlayer(Player player);

		//
		// delete methods

		void DeleteBoard(Board board);

		//
		// commit methods

		void Save();

		//
		// query methods

		Board GetBoardById(int id);
		User GetBoardOwner(Board board);

		IQueryable<Board> GetBoards();
		IQueryable<Board> GetBoards(Game game);
		IQueryable<Board> GetBoards(User user);
		IQueryable<Board> GetBoards(Game game, User user);
		IQueryable<User> GetBoardUsers(Board board);	
	}
}