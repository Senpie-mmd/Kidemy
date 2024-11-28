using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
	public class HeaderUsefulLinksViewComponent : ViewComponent
	{
		#region Fields

		private readonly ILinkService _linkService;
		private readonly ILogger<HeaderUsefulLinksViewComponent> _logger;

		#endregion

		#region Ctor

		public HeaderUsefulLinksViewComponent(ILinkService linkService, ILogger<HeaderUsefulLinksViewComponent> logger)
		{
			_linkService = linkService;
			_logger = logger;
		}

		#endregion

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var result = await _linkService.GetAllLinksForHeader();

			if (result.IsFailure)
			{
				_logger.LogError("get links is failed");
			}

			return View("HeaderUsefulLinks", result.Value);
		}
	}
}
