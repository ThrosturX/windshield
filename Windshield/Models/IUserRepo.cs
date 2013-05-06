using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IUserRepo
	{
		void AddUser(User user);
		void DeleteUser(User user);
		void AddAdmin(User user);
		void RemoveAdmin(User user);
		uint GetTimesPlayed(User user);
		uint GetTimesPlayedByGame(User user, GameType game);
		List<Group> GetGroupsByUser(User user);
		List<User> GetGroupMembers(Group group);
		List<User> GetFriends(User user);
		List<GameRating> GetGameRatings(User user);
		GameRating GetGameRatingByGame(User user, GameType game);
	}
}