using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public class UserDetailsRepo :IUserDetailsRepo
	{
		public static BoardGamesDataContext db = null;

		public UserDetailsRepo()
		{
			if (db == null)
			{
				db = new BoardGamesDataContext();
			}
		}

		public void ChangeUserDetails(UserDetail userDetail)
		{
			db.UserDetails.InsertOnSubmit(userDetail);
		}

		public void Save()
		{
			db.SubmitChanges();
		}
	}
}