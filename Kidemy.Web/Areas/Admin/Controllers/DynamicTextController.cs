using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.DynamicText;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageDynamicTexts")]
    public class DynamicTextController : BaseAdminController
    {
        #region Fields

        private readonly IDynamicTextService _dynamicTextService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public DynamicTextController(IDynamicTextService dynamicTextService, IStringLocalizer<SharedResource> localizer)
        {
            _dynamicTextService = dynamicTextService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [Permission("DynamicTextsList")]
        public async Task<IActionResult> List(FilterDynamicTextViewModel filter)
        {
            filter.TakeEntity = 20;

            var result = await _dynamicTextService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View(result.Value);

        }

        #endregion

        #region Update

        [Permission("UpdateDynamicText")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _dynamicTextService.GetDynamicTextForEditByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List), new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("UpdateDynamicText")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpdateDynamicTextViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _dynamicTextService.UpdateAsync(model);

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

        #endregion
    }
}
