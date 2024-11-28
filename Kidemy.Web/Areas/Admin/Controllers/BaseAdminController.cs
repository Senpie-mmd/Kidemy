using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class BaseAdminController : Controller
	{
		public static string SuccessMessage = "SuccessMessage";
		public static string ErrorMessage = "ErrorMessage";
		public static string InfoMessage = "InfoMessage";
		public static string WarningMessage = "WarningMessage";
	}
}
