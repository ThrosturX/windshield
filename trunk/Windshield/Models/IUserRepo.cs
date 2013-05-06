using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IUserRepo
	{
		void AddUser(UserProfile user);
		void DeleteUser(UserProfile user);
		void AddAdmin(UserProfile user);
		void RemoveAdmin(UserProfile user);
		uint GetTimesPlayed(UserProfile user);
		uint GetTimesPlayedByGame(UserProfile user, GameType game);
		List<Group> GetGroupsByUser(UserProfile user);
		List<UserProfile> GetGroupMembers(Group group);
		List<UserProfile> GetFriends(UserProfile user);
		List<GameRating> GetGameRatings(UserProfile user);
		GameRating GetGameRatingByGame(UserProfile user, GameType game);
	}
}