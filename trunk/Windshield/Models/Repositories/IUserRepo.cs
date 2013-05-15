using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IUserRepo
	{
		void Save();
		int GetTimesPlayed(User user);
		int GetTimesPlayedByGame(User user, Game game);
		User GetUserByName(string name);
		IQueryable<Group> GetGroupsByUser(User user);
		IQueryable<User> GetGroupMembers(Group group);
		IQueryable<User> GetFriends(User user);
		IQueryable<GameRating> GetGameRatings(User user);
		IQueryable<User> GetAllUsers();
		IQueryable<User> GetTopUsersByGame(Game game);
		GameRating GetGameRatingByGame(User user, Game game);

	}
}