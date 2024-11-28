using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Banner;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageBanners")]
    public class BannerController : BaseAdminController
    {
        #region Fields

        private readonly IBannerService _bannerService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public BannerController(IBannerService bannerService, IStringLocalizer<SharedResource> localizer)
        {
            _bannerService = bannerService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [Permission("BannersList")]
        public async Task<IActionResult> List(FilterBannerViewModel model)
        {
            model.TakeEntity = 20;
            var result = await _bannerService.FilterAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return View(result.Value);
        }

        #endregion

        #region Create

        [Permission("CreateBanner")]
        public IActionResult Create()
        {
            return View(new AdminSideCreateBannerViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("CreateBanner")]
        public async Task<IActionResult> Create(AdminSideCreateBannerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _bannerService.CreateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction("List", "Banner", new { area = "Admin" });
        }

        #endregion

        #region Update

        [Permission("UpdateBanner")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _bannerService.GetBannerForEditById(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List));
            }

            return View(result.Value);
        }

        [Permission("UpdateBanner")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpdateBannerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _bannerService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Delete

        [Permission("DeleteBanner")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var result = await _bannerService.DeleteAsync(id);


            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #endregion
    }
}
