using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.ViewModels
{
	public class ErrorViewModel
	{
		public string Error { get; set; }
		public string Message { get; set; }

		public ErrorViewModel(string error, string message)
		{
			Error = error;
			Message = message;
		}

	}
}