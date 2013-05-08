using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public class UserRepo : IUserRepo
	{

		public static BoardGamesDataContext db = null;

		public UserRepo()
		{
			if (db == null)
			{
				db = new BoardGamesDataContext();
			}
		}

		public uint GetTimesPlayed(User user)
		{
			return 1;
		}

		public uint GetTimesPlayedByGame(User user, Game game)
		{
			return 2;
		}

		public IQueryable<Group> GetGroupsByUser(User user)
		{
			return from groups in db.Groups
				   select groups;
		}

		public IQueryable<aspnet_User> GetGroupMembers(Group group)
		{
			return from users in db.aspnet_Users
				   select users;
		}

		public IQueryable<aspnet_User> GetFriends(User user)
		{
			return from users in db.aspnet_Users
				   select users;
		}

		public IQueryable<GameRating> GetGameRatings(User user)
		{
			return from gameRatings in db.GameRatings
				   select gameRatings;
		}

		public GameRating GetGameRatingByGame(User user, Game game)
		{
			return (from gameRatings in db.GameRatings
				   select gameRatings).FirstOrDefault();
		}
	}
}