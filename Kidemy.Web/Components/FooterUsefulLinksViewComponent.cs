using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class FooterUsefulLinksViewComponent : ViewComponent
    {
        #region Fields

        private readonly ILinkService _linkService;
        private readonly ILogger<FooterUsefulLinksViewComponent> _logger;

        #endregion

        #region Ctor

        public FooterUsefulLinksViewComponent(ILinkService linkService, ILogger<FooterUsefulLinksViewComponent> logger)
        {
            _linkService = linkService;
            _logger = logger;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _linkService.GetAllLinksForFooter();

            if (result.IsFailure)
            {
                _logger.LogError("get links is failed");
            }

            return View("FooterUsefulLinks", result.Value);
        }
    }
}
