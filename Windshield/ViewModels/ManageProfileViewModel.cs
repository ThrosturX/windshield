﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windshield.Models;

namespace Windshield.ViewModels
{
	public class ManageProfileViewModel
	{
		public User currentUserModel { get; set; }
		public UserDetail changeUserDetailsModel { get; set; }
		public ChangePasswordModel changePasswordModel { get; set; }
	}
}