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
		List<Group> GetGroupsByUser(UserProfile user);
		List<UserProfile> GetFriends(UserProfile user);
		GameRating GetGameRating(UserProfile user, GameType game);
		List<GameRating> GetGameRatigns(UserProfile user);
		uint GetTimesPlayed(UserProfile user);
		uint GetTimesPlayedByGame(UserProfile, GameType game);
		List<UserProfile> GetGroupMembers(Group group);
		void AddAdmin(UserProfile user);
		void RemoveAdmin(UserProfile user);
	}
}