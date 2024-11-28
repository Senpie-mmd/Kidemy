using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class AboutUsController : BaseController
    {
        #region Fields

        private readonly IAboutUsService _aboutUsService;
        private readonly IStringLocalizer _localizer;

        #endregion

        #region Constructor

        public AboutUsController(IAboutUsService aboutUsService,
            IStringLocalizer localizer)
        {
            _aboutUsService = aboutUsService;
            _localizer = localizer;

        }

        #endregion

        #region Actions

        [Route("about-us")]
        public async Task<IActionResult> Index()
        {
            var result = await _aboutUsService.GetAboutUsPageForUser();

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return Redirect("/");
            }


            return View(result.Value);
        }
        #endregion
    }
}