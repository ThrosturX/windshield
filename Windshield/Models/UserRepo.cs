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

		public int GetTimesPlayed(User user)
		{
			return (from gameRating in db.GameRatings
					where user.UserId == gameRating.idUser
					select gameRating.rating).Sum();
		}

		public int GetTimesPlayedByGame(User user, Game game)
		{
			return (from gameRatings in db.GameRatings
				    where gameRatings.idUser == user.UserId && gameRatings.idGame == game.id
				    select gameRatings.rating).SingleOrDefault(); 
		}

		public User GetUserByName(string name)
		{
			return (from users in db.Users
					where users.UserName == name
					select users).SingleOrDefault();
		}

		public IQueryable<Group> GetGroupsByUser(User user)
		{
			return from groupMembers in db.GroupMembers
				   where groupMembers.idUser == user.UserId
				   select groupMembers.Group;
		}

		public IQueryable<User> GetGroupMembers(Group g)
		{
			return from groupMembers in db.GroupMembers
				   where groupMembers.idGroup == g.id
				   select groupMembers.User;
		}

		public IQueryable<User> GetFriends(User user)
		{
			return from friends in db.Friends
				   where friends.idOne == user.UserId
				   select friends.User;
		}

		public IQueryable<GameRating> GetGameRatings(User user)
		{
			return from gameRatings in db.GameRatings
				   where gameRatings.idUser == user.UserId
				   select gameRatings;
		}

		public GameRating GetGameRatingByGame(User user, Game game)
		{
			return (from gameRatings in db.GameRatings
					where gameRatings.idUser == user.UserId && gameRatings.idGame == game.id
				   select gameRatings).FirstOrDefault();
		}
	}
}