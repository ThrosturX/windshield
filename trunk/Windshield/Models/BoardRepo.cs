using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public class BoardRepo : IBoardRepo
	{
		public static BoardGamesDataContext db = null;

		public BoardRepo()
		{
			if (db == null)
			{
				db = new BoardGamesDataContext();
			}
		}

		public void AddBoard(Board board)
		{
			db.Boards.InsertOnSubmit(board);
		}

		public void DeleteBoard(Board board)
		{
			db.Boards.DeleteOnSubmit(board);
		}

		public IQueryable<Board> GetBoards()
		{
			return from board in db.Boards
				   select board;
		}

		public IQueryable<Board> GetBoards(Game game)
		{
			return from board in db.Boards
				   where board.idGame == game.id
				   select board;
		}

		public IQueryable<aspnet_User> GetBoardUsers(Board board)
		{
			return from player in db.Players
				   where player.idBoard == board.id
				   select player.aspnet_User;
		}

		public Game GetGameType(Board board)
		{
			return (from game in db.Games
				   where board.idGame == game.id
				   select game).SingleOrDefault();
		}
		public aspnet_User GetBoardOwner(Board board)
		{
			return (from user in db.aspnet_Users
					where user.UserId == board.idOwner
					select user).SingleOrDefault();
		}
	}
}