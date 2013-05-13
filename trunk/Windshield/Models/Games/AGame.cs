using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games
{
	public abstract class AGame
	{
		public int id;
		public virtual int TryAction(string action, string sender)
		{
			return 0;
		}
	}

}