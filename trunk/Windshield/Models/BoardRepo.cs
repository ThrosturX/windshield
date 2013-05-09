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

		//
		// insert methods

		/// <summary>
		/// Adds the board to the table
		/// </summary>
		public void AddBoard(Board board)
		{
			db.Boards.InsertOnSubmit(board);
		}

		//
		// delete methods

		/// <summary>
		/// Deletes the board from the table
		/// </summary>
		public void DeleteBoard(Board board)
		{
			db.Boards.DeleteOnSubmit(board);
		}

		//
		// commit methods

		/// <summary>
		/// Submits all changes done to the database
		/// </summary>
		public void Save()
		{
			db.SubmitChanges();
		}

		//
		// query methods
		
		/// <summary>
		/// Returns all boards from the Boards table
		/// </summary>
		public IQueryable<Board> GetBoards()
		{
			return from board in db.Boards
				   select board;
		}

		/// <summary>
		/// Returns all boards for a specific game 
		/// </summary>
		public IQueryable<Board> GetBoards(Game game)
		{
			return from board in db.Boards
				   where board.idGame == game.id
				   select board;
		}

		/// <summary>
		/// Returns all users for a specific board
		/// </summary>
		public IQueryable<User> GetBoardUsers(Board board)
		{
			return from player in db.Players
				   where player.idBoard == board.id
				   select player.User;
		}


		public User GetBoardOwner(Board board)
		{
			return (from user in db.Users
					where user.UserId == board.idOwner
					select user).SingleOrDefault();
		}
	}
}