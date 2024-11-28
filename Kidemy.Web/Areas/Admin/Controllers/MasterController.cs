using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Application.ViewModels.SettlementTransaction;
using Kidemy.Domain.Enums.Master;
using Kidemy.Domain.Models.User;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageMasters")]
    public class MasterController : BaseAdminController
    {
        #region Fields

        private readonly IMasterService _masterService;

        private readonly IStringLocalizer _localizer;

        #endregion

        #region Constructor

        public MasterController(IMasterService masterService, IStringLocalizer localizer)
        {
            _masterService = masterService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [Permission("MasterList")]
        public async Task<IActionResult> List(FilterForAdminSideMasterViewModel model)
        {
            var result = await _masterService.FilterForAdminSideAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return View(result.Value);
        }

        #endregion

        #region ChangeStatus

        [HttpPost]
        [Permission("MasterDetails")]
        public async Task<IActionResult> ChangeStatus(int id, MasterStatus Status)
        {

            var result = await _masterService.ChangeStatusAsync(id, Status);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List));
            }

            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction("Profile", "User", new { id = id });

        }

        #endregion

        #region Set Blocked Amount
        [HttpGet]
        [Permission("SetBlockedAmount")]
        public async Task<IActionResult> SetBlockedAmount(int id)
        {
            var result = await _masterService.GetBlockedBalanceAsync(id);

            return result.IsSuccess ? PartialView("_SetBlockedAmount", result.Value) : PartialView("_SetBlockedAmount");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("SetBlockedAmount")]
        public async Task<IActionResult> SetBlockedAmount(BlockedAmountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values
                    .First(v => v.ValidationState is ModelValidationState.Invalid)
                    .Errors
                    .First()
                    .ErrorMessage;

                TempData[ErrorMessage] = message;
                return RedirectToAction("Profile", "User", new { id = model.UserId });
            }

            var result = await _masterService.SetSetBlockedAmount(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            }

            return RedirectToAction("Profile", "User", new { id = model.UserId });
        }


       

        #endregion

        #endregion
    }
}
