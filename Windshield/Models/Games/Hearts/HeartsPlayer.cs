using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

namespace Windshield.Games.Hearts
{
	public class HeartsPlayer
	{
		public User user;
		public uint gamePoints;
		public uint matchPoints;
		public HeartsHand hand;
	}
}