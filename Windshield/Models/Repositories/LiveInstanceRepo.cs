using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.ViewModels;

namespace Windshield.Models
{
	public class LiveInstanceRepo
	{
		public static List<BoardViewModel> gameInstances = null;

		public LiveInstanceRepo()
		{
			if (gameInstances == null)
				gameInstances = new List<BoardViewModel>();
		}

		public void Add(BoardViewModel theInstance)
		{
			gameInstances.Add(theInstance);
		}

		public BoardViewModel GetInstanceByID(int id)
		{
			return (from instance in gameInstances
					where instance.id == id
					select instance).SingleOrDefault();
		}
	}
}