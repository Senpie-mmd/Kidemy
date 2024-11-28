using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class FooterPagesViewComponent : ViewComponent
    {
        #region Fields

        private readonly IPageService _pageService;
        private readonly ILogger<FooterPagesViewComponent> _logger;

        #endregion

        #region Ctor

        public FooterPagesViewComponent(IPageService pageService, ILogger<FooterPagesViewComponent> logger)
        {
            _pageService = pageService;
            _logger = logger;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _pageService.GetAllPagesForFooter();

            if (result.IsFailure)
            {
                _logger.LogError("get links is failed");
            }

            return View("FooterPages", result.Value);
        }
    }
}
