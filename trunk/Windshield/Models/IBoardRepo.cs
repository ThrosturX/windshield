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
		IEnumerable<Board> GetBoards();
		IEnumerable<Board> GetBoards(Game type);
		IEnumerable<User> GetBoardUsers(Board board);
		Game GetGameType(Board board);
		User GetBoardOwner(Board board);
	}
}