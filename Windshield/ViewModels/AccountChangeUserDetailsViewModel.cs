using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

namespace Windshield.ViewModels
{
	public class AccountChangeUserDetailsViewModel
	{
		public User currentUserModel { get; set; }
		public ChangeUserDetailsModel changeUserDetailsModel { get; set; }
		public ChangePasswordModel changePasswordModel { get; set; }
	}
}