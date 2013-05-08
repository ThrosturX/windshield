using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public class UserRepo : IUserRepo
	{
		uint GetTimesPlayed(User user)
		{
			return 1;
		}
		uint GetTimesPlayedByGame(User user, Game game);
		IQueryable<Group> GetGroupsByUser(User user);
		IQueryable<User> GetGroupMembers(Group group);
		IQueryable<User> GetFriends(User user);
		IQueryable<GameRating> GetGameRatings(User user);
		GameRating GetGameRatingByGame(User user, Game game);
	}
}