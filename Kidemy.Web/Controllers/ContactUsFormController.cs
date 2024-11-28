using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.ContactUs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class ContactUsFormController : BaseController
    {
        #region Fields

        private readonly IContactUsFormService _contactUsFormService;
        private readonly IStringLocalizer _localizer;

        #endregion

        #region Ctor


        public ContactUsFormController(IContactUsFormService contactUsFormService,
            IStringLocalizer localizer)
        {
            _contactUsFormService = contactUsFormService;
            _localizer = localizer;
        }


        #endregion

        [HttpGet("/contact-us")]
        public IActionResult CreateContactUsForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost("/contact-us"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContactUsForm(ClientSideCreateContactUsFormViewModel model)
        {
            model.UserId = User.GetUserId();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _contactUsFormService.CreateContactUsForm(model);

            if (result.IsFailure)
                TempData[ErrorMessage] = _localizer[result.Message].ToString();

            else
                TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(CreateContactUsForm), new { area = "" });
        }

    }
}
