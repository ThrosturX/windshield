using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Security;

namespace Windshield.Models
{

	public class ChangePasswordModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Current password")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "New password")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm new password")]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[DataType(DataType.Upload)]
		[Display(Name = "Upload Avatar")]
		public string UploadAvatar { get; set; }
	}

	public class LoginModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}

	public class RegisterModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email address")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class ChangeUserDetailsModel
	{
		[DataType(DataType.Text)]
		[Display(Name = "Gender")]
		public string Gender { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "Age")]
		public string Age { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "Settings")]
		public string Settings { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "Occupation")]
		public string Occupation { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "Country")]
		public string Country { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "Avatar")]
		public string Avatar { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "UserRating")]
		public string UserRating { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "DateJoined")]
		public string DateJoined { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "Name")]
		public string Name { get; set; }

	}
}
