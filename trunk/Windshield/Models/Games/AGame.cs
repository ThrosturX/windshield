﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models.Games
{
	public abstract class AGame
	{
		public virtual Board NewBoard()
		{
			return new Board();
		}

		public virtual bool TryAction(string action, string sender)
		{
			return false;
		}
	}

}