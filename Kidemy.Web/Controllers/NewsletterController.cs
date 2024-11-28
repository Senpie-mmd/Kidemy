using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Newsletter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class NewsletterController : BaseController
    {
        #region Fields
        private readonly INewsletterService _newslettersService;
        private readonly IStringLocalizer _localizer;
        #endregion

        #region Ctor
        public NewsletterController(INewsletterService newslettersService,
            IStringLocalizer localizer)
        {
            _newslettersService = newslettersService;
            _localizer = localizer;
        }
        #endregion

        #region Actions

        #region Register

        [HttpPost("register-newsletter")]
        public async Task<IActionResult> Register(RegisterNewsletterViewModel newsletters)
        {

            var result = await _newslettersService.RegisterNewsletterMembership(newsletters);

            if (result.IsSuccess)
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion 

        #endregion
    }
}
