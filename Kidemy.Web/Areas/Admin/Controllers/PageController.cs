using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManagePages")]
    public class PageController : BaseAdminController
    {
        #region Fields

        private readonly IPageService _pageService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public PageController(IPageService pageService, IStringLocalizer<SharedResource> localizer)
        {
            _pageService = pageService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [Permission("PagesList")]
        public async Task<IActionResult> List(FilterPageViewModel filter)
        {
            var result = await _pageService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View(result.Value);

        }
        #endregion

        #region Create

        [Permission("CreatePage")]
        public IActionResult Create()
        {
            return View(new AdminSideUpsertPageViewModel());

        }

        [Permission("CreatePage")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideUpsertPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _pageService.CreateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(List), new { area = "Admin" });

        }

        #endregion

        #region Update

        [Permission("UpdatePage")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _pageService.GetPageForEditByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List), new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("UpdatePage")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpsertPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _pageService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(List), new { area = "Admin" });

        }
        #endregion

        #region Delete

        [Permission("DeletePage")]
        public async Task<ResponseResult> Delete(int id)
        {
            if (id < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _pageService.DeleteAsync(id);

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

        #endregion

        #endregion
    }
}
