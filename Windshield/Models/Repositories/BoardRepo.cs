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
		//adds player to the table

		public void AddPlayer(Player player)
		{
			db.Players.InsertOnSubmit(player);
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

		public Board GetBoardById(int id)
		{
			return (from board in db.Boards
					where board.id == id
					select board).SingleOrDefault();
		}

		/// <summary>
		/// Returns all boards from the Boards table; the most recent boards first
		/// </summary>
		public IQueryable<Board> GetBoards()
		{
			return from board in db.Boards
				   orderby board.id descending
				   select board;
		}

		/// <summary>
		/// Returns all boards for a specific game ; the most recent boards first
		/// </summary>
		public IQueryable<Board> GetBoards(Game game)
		{
			return from board in db.Boards
				   where board.idGame == game.id
				   orderby board.id descending
				   select board;
		}

		/// <summary>
		/// Get all the active boards for a specific user; the most recent ones first
		/// </summary>
		public IQueryable<Board> GetBoards(User user)
		{
			var playerInGames = from players in db.Players
								where players.UserName == user.UserName && players.Board.endDate == null
								orderby players.idBoard descending
								select players.Board;
			return playerInGames;
		}

		/// <summary>
		/// Get all the active boards for a specific user and game; the most recent ones first
		/// </summary>
		public IQueryable<Board> GetBoards(Game game, User user)
		{
			var result = from boards in GetBoards(user)
						 where boards.Game == game
						 orderby boards.id descending
						 select boards;
			return result;
		}


		/// <summary>
		/// Returns all users for a specific board in the order they were added to the board
		/// </summary>
		public IQueryable<User> GetBoardUsers(Board board)
		{
			return from player in db.Players
				   where player.idBoard == board.id
				   orderby player.playerNumber
				   select player.User;
		}

		/// <summary>
		/// Returns the owner of a specific board
		/// </summary>
		public User GetBoardOwner(Board board)
		{
			var result =  (from user in db.Users
			               where user.UserName == board.ownerName
			               select user).SingleOrDefault();
			return result;
		}
	}
}