using Kidemy.Application.ViewModels.Common;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Kidemy.Application.ViewModels.SocialMedia;
using Kidemy.Application.Services.Interfaces;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageSocialMedia")]
    public class SocialMediaController : BaseAdminController
    {
        #region Fields

        private readonly ISocialMediaService _socialMediaService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public SocialMediaController(ISocialMediaService socialMediaService, IStringLocalizer<SharedResource> localizer)
        {
            _socialMediaService = socialMediaService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        [Permission("SocialMediaList")]
        public async Task<IActionResult> List(FilterSocialMediaViewModel model)
        {
            var result = await _socialMediaService.FilterSocialMediaAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(model);
        }

        [Permission("CreateSocialMedia")]
        public IActionResult Create()
        {
            return View(new AdminSideUpsertSocialMediaViewModel());
        }

        [Permission("CreateSocialMedia")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideUpsertSocialMediaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _socialMediaService.CreateSocialMediaAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(List));
        }

        [Permission("UpdateSocialMedia")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _socialMediaService.GetSocialMediaById(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List));
            }

            return View(result.Value);
        }

        [Permission("UpdateSocialMedia")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpsertSocialMediaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _socialMediaService.UpdateSocialMediaAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(List));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("DeleteSocialMedia")]
        public async Task<ResponseResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return new ResponseResult(false);
            };

            var result = await _socialMediaService.DeleteSocialMediaAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return new ResponseResult(false);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return ResponseResult.Success();
        }

        #endregion
    }
}