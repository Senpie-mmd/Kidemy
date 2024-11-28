using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.AboutUs;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageAboutUs")]
    public class AboutUsController : BaseAdminController
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

        #region AboutUs
        [Permission("UpdateAboutUs")]
        public async Task<IActionResult> Update()
        {
            var result = await _aboutUsService.GetAboutUsAsync();

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(new AdminSideUpsertAboutUsViewModel());
            }

            return View(result.Value);

        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("UpdateAboutUs")]
        public async Task<IActionResult> Update(AdminSideUpsertAboutUsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _aboutUsService.UpdateAboutUsAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }


            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(Update));
        }
        #endregion

        #region ProgressBarCRUD
        [Permission("CreateProgressBar")]
        public IActionResult CreateProgressBar()
        {
            CreateAboutUsProgressBarViewModel model = new CreateAboutUsProgressBarViewModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("CreateProgressBar")]
        public async Task<IActionResult> CreateProgressBar(CreateAboutUsProgressBarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _aboutUsService.CreateAboutUsProgressBar(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(ProgressBarList));
        }

        [Permission("ProgressBarList")]
        public async Task<IActionResult> ProgressBarList(FilterProgressBarViewModel filter)
        {
            var result = await _aboutUsService.FilterProgressBarViewModel(filter);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(result.Value);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("DeleteProgressBar")]
        public async Task<ResponseResult> DeleteProgressBar(int id)
        {
            if (id is < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _aboutUsService.DeleteProgressBar(id);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }

        [Permission("UpdateProgresBar")]
        public async Task<IActionResult> UpdateProgresBar(int id)
        {
            if (id is < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return RedirectToAction(nameof(ProgressBarList));
            }

            var result = await _aboutUsService.GetProgressBar(id);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(ProgressBarList));
            }

            return View(result.Value);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("UpdateProgresBar")]
        public async Task<IActionResult> UpdateProgresBar(UpdateAboutUsProgressBarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _aboutUsService.UpdateProgressBar(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(ProgressBarList));
        }
        #endregion
    }
}
