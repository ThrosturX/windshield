using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public class LiveInstanceRepo
	{
		public static List<GameInstance> gameInstances = null;

		public LiveInstanceRepo()
		{
			if (gameInstances == null)
				gameInstances = new List<GameInstance>();
		}

		public void Add(GameInstance theInstance)
		{
			gameInstances.Add(theInstance);
		}

		public GameInstance GetInstanceByID(int id)
		{
			return (from instance in gameInstances
					where instance.id == id
					select instance).SingleOrDefault();
		}
	}
}