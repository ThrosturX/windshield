﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IBoardRepo
	{
		void AddBoard(Board board);
		void DeleteBoard(Board board);
		IQueryable<Board> GetBoards();
		IQueryable<Board> GetBoards(Game game);
		IQueryable<User> GetBoardUsers(Board board);
		User GetBoardOwner(Board board);
	}
}