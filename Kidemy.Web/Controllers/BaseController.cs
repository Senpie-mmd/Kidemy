using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Controllers
{
	public class BaseController : Controller
	{
		public static string SuccessMessage = "SuccessMessage";
		public static string ErrorMessage = "ErrorMessage";
		public static string InfoMessage = "InfoMessage";
		public static string WarningMessage = "WarningMessage";
    }
}
