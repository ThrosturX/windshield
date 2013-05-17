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