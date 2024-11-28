using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.ContactUs;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageContactUs")]
    public class ContactUsController : BaseAdminController
    {
        #region Fields

        private readonly IContactUsFormService _contactUsFormService;
        private readonly IContactUsService _contactUsService;
        private readonly IStringLocalizer _localizer;

        #endregion

        #region Ctor

        public ContactUsController(IContactUsService contactUsService,
            IContactUsFormService contactUsFormService,
            IStringLocalizer localizer)
        {
            _contactUsService = contactUsService;
            _contactUsFormService = contactUsFormService;
            _localizer = localizer;
        }


        #endregion

        #region Actions

        [HttpGet]
        [Permission("UpdateContactUs")]
        public async Task<IActionResult> UpdateContactUs()
        {
            var result = await _contactUsService.GetContactUs();

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController), new { area = "Admin" });
            }

            return View(result.Value);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("UpdateContactUs")]
        public async Task<IActionResult> UpdateContactUs(AdminSideUpsertContactUsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _contactUsService.UpsertContactUs(model);

            if (result.IsFailure)
                TempData[ErrorMessage] = _localizer[result.Message].ToString();

            else
                TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(UpdateContactUs));
        }

        [Permission("ContactUsFormList")]
        public async Task<IActionResult> ContactUsFormList(FilterContactUsFormViewModel filter)
        {
            var result = await _contactUsFormService.FilterContactUsForm(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController), new { area = "Admin" });
            }

            return View(result.Value);
        }

        [HttpGet]
        [Permission("ContactUsFormReply")]
        public async Task<IActionResult> ContactUsFormReply(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.NullValue].ToString();
                return RedirectToAction(nameof(ContactUsFormList), new { area = "Admin" });
            }

            var result = await _contactUsFormService.GetContactUsFormById(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(ContactUsFormList), new { area = "Admin" });
            }

            ViewData["ContactUsForm"] = result.Value;

            return View(new AdminSideAnswerContactUsFormViewModel { ContactUsFormId = id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("ContactUsFormReply")]
        public async Task<IActionResult> ContactUsFormReply(AdminSideAnswerContactUsFormViewModel model)
        {
            model.AnswererId = User.GetUserId();
            if (!ModelState.IsValid)
            {
                ViewData["ContactUsForm"] = (await _contactUsFormService.GetContactUsFormById(model.ContactUsFormId)).Value;

                return View(model);
            }

            var result = await _contactUsFormService.AnswerContactUsForm(model);

            if (result.IsFailure)
                TempData[ErrorMessage] = _localizer[result.Message].ToString();

            else
                TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(ContactUsFormList), new { area = "Admin" });
        }

        [HttpGet]
        [Permission("DeleteContactUsForm")]
        public async Task<IActionResult> DeleteContactUsForm(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.NullValue].ToString();
                return RedirectToAction(nameof(ContactUsFormList), new { area = "Admin" });
            }

            var result = await _contactUsFormService.DeleteContactUsForm(id);

            if (result.IsFailure)
                TempData[ErrorMessage] = _localizer[result.Message].ToString();

            else
                TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(ContactUsFormList), new { area = "Admin" });
        }

        #endregion
    }
}
