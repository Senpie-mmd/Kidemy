using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.VIPPlan;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageVIPServices")]
    public class VIPPlanController : BaseAdminController
    {
        #region Fields

        private readonly IVIPPlanService _vIPPlanService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public VIPPlanController(IVIPPlanService vIPPlanService, IStringLocalizer<SharedResource> localizer)
        {
            _vIPPlanService = vIPPlanService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [Permission("VIPPlansList")]
        public async Task<IActionResult> List(AdminSideFilterVIPPlanViewModel filter)
        {
            var result = await _vIPPlanService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(result.Value);

        }

        #endregion

        #region Create

        [Permission("CreateVIPPlan")]
        public IActionResult Create()
        {
            return View(new AdminSideUpsertVIPPlanViewModel());

        }

        [Permission("CreateVIPPlan")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideUpsertVIPPlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _vIPPlanService.CreateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction(nameof(List));

        }

        #endregion

        #region Update

        [Permission("UpdateVIPPlan")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _vIPPlanService.GetVIPPlanForEditByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List), new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("UpdateVIPPlan")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpsertVIPPlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _vIPPlanService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }


            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction(nameof(List));

        }

        #endregion

        #region Delete

        [Permission("DeleteVIPPlan")]
        public async Task<ResponseResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return new ResponseResult(false);
            }

            var result = await _vIPPlanService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return new ResponseResult(false);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion 

        #endregion
    }
}
