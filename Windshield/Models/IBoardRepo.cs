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
		IEnumerable<Board> GetBoards(GameType type);
		IEnumerable<UserProfile> GetBoardUsers(Board board);
		GameType GetGameType(Board board);
		UserProfile GetBoardOwner(Board board);
	}
}