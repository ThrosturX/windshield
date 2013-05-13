using System;
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
		
		public int GetTimesPlayed(User user)
		{
			// dummy
			return 0;
		}

		public int GetTimesPlayedByGame(User user, Game game)
		{
			// dummy
			return 0;
		}

		public User GetUserByName(string name)
		{
			return (from users in rep
					where users.UserName == name
					select users).SingleOrDefault();
		}

		public IQueryable<Group> GetGroupsByUser(User user)
		{
			// dummy
			return null;
		}

		public IQueryable<User> GetGroupMembers(Group group)
		{
			// dummy
			return null;
		}

		public IQueryable<User> GetFriends(User user)
		{
			// dummy
			return null;
		}

		public IQueryable<GameRating> GetGameRatings(User user)
		{
			// dummy
			return null;
		}

		public IQueryable<User> GetTopUsersByGame(Game game)
		{
			// dummy
			return null;
		}

		public IQueryable<User> GetAllUsers()
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
