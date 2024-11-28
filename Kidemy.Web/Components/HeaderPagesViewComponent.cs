using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
	public class HeaderPagesViewComponent : ViewComponent
	{
        #region Fields

        private readonly IPageService _pageService;
        private readonly ILogger<HeaderPagesViewComponent> _logger;

        #endregion

        #region Ctor

        public HeaderPagesViewComponent(IPageService pageService, ILogger<HeaderPagesViewComponent> logger)
        {
            _pageService = pageService;
            _logger = logger;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _pageService.GetAllPagesForHeader();

            if (result.IsFailure)
            {
                _logger.LogError("get links is failed");
            }

            return View("HeaderPages", result.Value);
        }
    }
}
