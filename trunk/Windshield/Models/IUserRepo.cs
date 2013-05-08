﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IUserRepo
	{
		// TODO: possibly remove when aspnet_User is clear
		/*
		void AddUser(User user);
		void DeleteUser(User user);
		void AddAdmin(User user);
		void RemoveAdmin(User user);
		*/
		uint GetTimesPlayed(User user);
		uint GetTimesPlayedByGame(User user, Game game);
		IQueryable<Group> GetGroupsByUser(User user);
		IQueryable<User> GetGroupMembers(Group group);
		IQueryable<User> GetFriends(User user);
		IQueryable<GameRating> GetGameRatings(User user);
		GameRating GetGameRatingByGame(User user, Game game);
	}
}