using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Windshield.Models;
using Windshield.ViewModels;

namespace Windshield.Controllers
{

	[Authorize]
	public class AccountController : Controller
	{
		private IUserRepo userRepository = null;
		private IUserDetailsRepo userDetailsRepository = null;

		public AccountController()
		{
			userRepository = new UserRepo();
			userDetailsRepository = new UserDetailsRepo();
		}

		//
		// GET: /Account/Index

		public ActionResult Index()
		{
			return View();
		}

		//
		// GET: /Account/Login

		[AllowAnonymous]
		public ActionResult Login()
		{
			return View();
		}

		//
		// POST: /Account/Login

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Login(LoginModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(model.UserName, model.Password))
				{
					FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
					if (Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		//
		// GET: /Account/LogOff

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "Home");
		}

		//
		// GET: /Account/Register

		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		//
		// POST: /Account/Register

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				// Attempt to register the user
				MembershipCreateStatus createStatus;
				Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

				if (createStatus == MembershipCreateStatus.Success)
				{
					FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", ErrorCodeToString(createStatus));
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		//
		// GET: /Account/ChangePassword

		public ActionResult ChangePassword()
		{
			AccountChangeUserDetailsViewModel model = new AccountChangeUserDetailsViewModel();
			model.currentUserModel = userRepository.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString());
			model.changeUserDetailsModel = new ChangeUserDetailsModel();
			model.changePasswordModel = new ChangePasswordModel();
			return View("ChangePassword", model); //"ChangePassword" redundant but perhaps good practice
		}

		//
		// POST: /Account/ChangePassword

		[HttpPost]
		public ActionResult ChangeUserDetail(AccountChangeUserDetailsViewModel model)
		{
			if (ModelState.IsValid)
			{
				UserDetail userDetail = userRepository.GetUserByName(System.Web.HttpContext.Current.User.Identity.Name.ToString()).UserDetail;
				// Age
				string inputAge = model.changeUserDetailsModel.Age;
				int outputAge;
				if (int.TryParse(inputAge, out outputAge))
				{
					userDetail.age = outputAge;
				}
				// Avatar
				userDetail.avatar = model.changeUserDetailsModel.Avatar;
				// Country
				userDetail.country = model.changeUserDetailsModel.Country;
				// Date Joined
				// -never changes
				// Email
				userDetail.email = model.changeUserDetailsModel.Email;
				// Gender
				string inputGender = model.changeUserDetailsModel.Gender;
				bool outputGender;
				if (bool.TryParse(inputGender, out outputGender))
				{
					userDetail.gender = outputGender;
				}
				// Name
				userDetail.name = model.changeUserDetailsModel.Name;
				// Occupation
				userDetail.occupation = model.changeUserDetailsModel.Occupation;
				// Settings
				userDetail.settings = model.changeUserDetailsModel.Settings;
				// UserRating
				string inputUserRating = model.changeUserDetailsModel.UserRating;
				int outputUserRating;
				if (int.TryParse(inputUserRating, out outputUserRating))
				{
					userDetail.userRating = outputUserRating;
				}

				// Commit to database
				userDetailsRepository.Save();
				return RedirectToAction("Index", "Home");
			}

			// If we got this far, something failed, redisplay form
			return View("ChangePassword", model);
		}

		//
		// GET: /Account/ChangePasswordSuccess

		public ActionResult ChangePasswordSuccess()
		{
			return View();
		}

		#region Status Codes
		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// See http://go.microsoft.com/fwlink/?LinkID=177550 for
			// a full list of status codes.
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "User name already exists. Please enter a different user name.";

				case MembershipCreateStatus.DuplicateEmail:
					return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

				case MembershipCreateStatus.InvalidPassword:
					return "The password provided is invalid. Please enter a valid password value.";

				case MembershipCreateStatus.InvalidEmail:
					return "The e-mail address provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidAnswer:
					return "The password retrieval answer provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidQuestion:
					return "The password retrieval question provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidUserName:
					return "The user name provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.ProviderError:
					return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				case MembershipCreateStatus.UserRejected:
					return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				default:
					return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
			}
		}
		#endregion
	}
}
