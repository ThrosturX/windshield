using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;


namespace Windshield.Models.Games
{
	public class IGameBinder
	{
		private static Assembly executingAssembly = Assembly.GetExecutingAssembly();

		public static IGame GetGameObjectFor(string gameModel)
		{
			Type gameType = executingAssembly.GetType("Windshield.Models.Games." + gameModel);
			object gameInstance = Activator.CreateInstance(gameType);
			return (IGame)gameInstance;
		}
	}
}


// RedirectToAction
// TODO: Latebindinglol
/*	owner.Players.Clear();
	Player player = new Player();
	player.dateJoined = DateTime.Now;
	player.playerNumber = 0;

	game.Boards.Add(board);
	board.Players.Add(player);

	owner.Players.Add(player);

	boardRepo.Save();
	foreach (var f in owner.Players)
	{
		//if(f.idUser == owner.UserId && f.Board.Game.id == game.id)
		System.Diagnostics.Debug.WriteLine(f.dateJoined);
	}
	return View("GameLobby");
	// RedirectToAction
}

/*
 * Assembly executingAssembly = Assembly.GetExecutingAssembly();
	Type gameType = executingAssembly.GetType("Windshield.Models.Games." + game.model);

	//object gameInstance = Activator.CreateInstance(gameType); <-- óþarfi
	MethodInfo getFullNameMethod = gameType.GetMethod("GetFullName");
*/