using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.SettlementTransaction;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageSettlementTransaction")]
    public class SettlementTransactionController : BaseAdminController
    {
        #region Fields
        private readonly ISettlementTransactionService _settlementTransactionService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion

        #region Constructor
        public SettlementTransactionController(ISettlementTransactionService settlementTransactionService, IStringLocalizer<SharedResource> localizer)
        {
            _settlementTransactionService = settlementTransactionService;
            _localizer = localizer;
        }
        #endregion

        #region Actions

        #region Create
        [HttpPost, ValidateAntiForgeryToken]
        [Permission("AddSettlementTransaction")]
        public async Task<IActionResult> AddSettlementTransaction(AdminSideUpsertSettlementTransactionViewModel model)
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

            var result = await _settlementTransactionService.CreateSettlementTransactionAsync(model);

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

        #region Edit

        [HttpGet]
        [Permission("EditSettlementTransaction")]
        public async Task<IActionResult> EditSettlementTransaction(int id)
        {
            var result = await _settlementTransactionService.GetSettlementTransactionForEditAsync(id);

            return result.IsSuccess ? PartialView("EditSettlementTransaction", result.Value) : PartialView("EditSettlementTransaction");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("EditSettlementTransaction")]
        public async Task<IActionResult> EditSettlementTransaction(AdminSideUpsertSettlementTransactionViewModel model)
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

            var result = await _settlementTransactionService.EditSettlementTransactionAsync(model);

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
