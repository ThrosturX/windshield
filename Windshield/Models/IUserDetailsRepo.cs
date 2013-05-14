using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
	public interface IUserDetailsRepo
	{
		void ChangeUserDetails(UserDetail userDetail);
		void Save();
	}
}