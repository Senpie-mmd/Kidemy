using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Controllers
{
	public class HandleErrorController : BaseController
	{
		public async Task<IActionResult> NotFound()
			=> View();

		public async Task<IActionResult> InternalError()
			=> View();
	}
}
