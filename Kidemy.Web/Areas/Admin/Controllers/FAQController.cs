using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.FAQ;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageFAQs")]
    public class FAQController : BaseAdminController
    {
        #region Fields

        private readonly IFAQService _fAQService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public FAQController(IFAQService fAQService, IStringLocalizer<SharedResource> localizer)
        {
            _fAQService = fAQService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [Permission("FAQsList")]
        public async Task<IActionResult> List(FilterFAQViewModel filter)
        {
            var result = await _fAQService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View(result.Value);

        }
        #endregion

        #region Create

        [Permission("CreateFAQ")]
        public IActionResult Create()
        {
            return View(new AdminSideUpsertFAQViewModel());

        }

        [Permission("CreateFAQ")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideUpsertFAQViewModel model)
        {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await _fAQService.CreateAsync(model);

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

        [Permission("UpdateFAQ")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _fAQService.GetFAQForEditByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List), new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("UpdateFAQ")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpsertFAQViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _fAQService.UpdateAsync(model);

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

        [Permission("DeleteFAQ")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var result = await _fAQService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        #endregion

        #endregion
    }
}
