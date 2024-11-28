using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
	[Area("UserPanel")]
	[Authorize]
	public class BaseUserPanelController : Controller
	{
		public readonly string SuccessMessage = "SuccessMessage";
		public readonly string InfoMessage = "InfoMessage";
		public readonly string ErrorMessage = "ErrorMessage";
		public readonly string WarningMessage = "WarningMessage";
	}
}
