﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windshield.Models;

namespace Windshield.Test.MockObjects
{
	internal class MockUserRepo : IUserRepo
	{
		public List<User> rep = new List<User>();

		public void AddUser(User user)
		{
			rep.Add(user);
		}

		public void DeleteUser(User user)
		{
			rep.Remove(user);
		}

		public void AddAdmin(User user)
		{
			// unimplemented
		}

		public void RemoveAdmin(User user)
		{
			// unimplemented
		}

		public uint GetTimesPlayed(User user)
		{
			// dummy
			return 0;
		}

		public uint GetTimesPlayedByGame(User user, Game game)
		{
			// dummy
			return 0;
		}

		public List<Group> GetGroupsByUser(User user)
		{
			// dummy
			return null;
		}

		public List<User> GetGroupMembers(Group group)
		{
			// dummy
			return null;
		}

		public List<User> GetFriends(User user)
		{
			// dummy
			return null;
		}

		public List<GameRating> GetGameRatings(User user)
		{
			// dummy
			return null;
		}

		public GameRating GetGameRatingByGame(User user, Game game)
		{
			// dummy
			return null;
		}

	}
}
