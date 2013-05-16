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
		public List<Board> boardTable = new List<Board>();
		public List<Player> playerTable = new List<Player>();
		public List<User> userTable = new List<User>();
	
		public void AddBoard(Board board)
		{
			boardTable.Add(board);
		}

		public void AddPlayer(Player player)
		{
			playerTable.Add(player);
		}

		public void Save()
		{
			// intentionally blank
		}

		public void DeleteBoard(Board board)
		{
			boardTable.Remove(board);
		}

		public Board GetBoardById(int id)
		{
			return (from board in boardTable
					where board.id == id
					select board).SingleOrDefault();
		}

		public IQueryable<Board> GetBoards()
		{
			return boardTable.AsQueryable();
		}

		public IQueryable<Board> GetBoards(Game type)
		{
			var result = from n in boardTable
						 where n.Game == type
						 select n;
			return result.AsQueryable();
		}

		public IQueryable<User> GetBoardUsers(Board board)
		{
			var result = from player in playerTable
						 where player.idBoard == board.id
						 select player.User;
			return result.AsQueryable();
		}

		public IQueryable<Board> GetBoards(User user)
		{
			var data = from players in playerTable
				       where players.UserName == user.UserName
					   select players;

			var result = from players in data
						 select players.Board;

			return result.AsQueryable();
		}

		public IQueryable<Board> GetBoards(Game game, User user)
		{
			var result = from boards in GetBoards(user)
						 where boards.idGame == game.id
						 select boards;
			return result;
		}

		public User GetBoardOwner(Board board)
		{
			var result = from user in userTable
						 where user.UserName == board.ownerName
						 select user;

			return result.SingleOrDefault();
		}
	}
}
