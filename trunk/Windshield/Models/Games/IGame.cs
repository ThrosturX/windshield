using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games
{
	/// <summary>
	/// A Game logic interface
	/// </summary>
	/// <remarks>
	/// Assume TicTacToe is a class that implements the IGame interface
	/// When creating a new board in database
	///   List<User> players = ...;  // some list of one or more
	///   var ttt = new TicTacToe();
	///   ttt.AddPlayers(players);
	///   Board board = new Board();
	///   board.status = ttt.GetStatus();
	///   board.idGame = ...;  // get id of the game from the repo
	///   boardRepo.AddBoard(board);
	///   boardRepo.Save()
	/// When getting the board from database
	///   Board board = ...;  // get board from repo
	///   var ttt = new TicTacToe();
	///   ttt.SetStatus(board.status);
	///   if (ttt.TryAction(...) == 1)
	///   {
	///	      board.status = ttt.GetStatus();
	///	      boardRepo.Save();
	///	      // broadcast with SignalR
	///	  }
	///   
	/// </remarks>
	public interface IGame
	{
		void AddPlayers(List<User> players);
		List<User> GetPlayers();
		string GetStatus();
		void SetStatus(string status);
		int TryAction(string action, string sender);
		string GetGameOver();
	}

}