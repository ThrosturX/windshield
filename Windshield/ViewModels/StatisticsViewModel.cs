﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	//Getting data for Statistics view.
	public class StatisticsViewModel
	{
		public string Name { get; set; }
		public int Rating { get; set; }
		public int TimesPlayed { get; set; }
	}
}