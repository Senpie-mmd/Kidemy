using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class PageController : BaseController
    {
        #region Fields

        private readonly IPageService _pageService;
        private readonly IStringLocalizer _localizer;

        #endregion

        #region Ctor

        public PageController(IPageService pageService,
            IStringLocalizer localizer)
        {
            _pageService = pageService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region Details

        [HttpGet("page-details/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (slug is null) return NotFound();

            var result = await _pageService.GetPageForUserBySlug(slug);

            if (result.IsFailure)
            {
                return NotFound();
            }

            return View(result.Value);
        }

        #endregion 

        #endregion
    }
}
