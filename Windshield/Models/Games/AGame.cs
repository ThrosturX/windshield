using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games
{
	public abstract class AGame
	{
		public virtual bool TryMove(Board board, string move)
		{
			return false;
		}
	}

}