using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games
{
	public abstract class AGame
	{
		/// <summary>
		/// Update the status string of the board
		/// </summary>
		public virtual void Update()
		{
			;  // intentionally empty
		}

		public virtual bool TryAction(string action, string sender)
		{
			return false;
		}
	}

}