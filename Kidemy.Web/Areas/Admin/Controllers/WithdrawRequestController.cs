using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.WithrawRequest;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageWithdrawRequests")]
    public class WithdrawRequestController : BaseAdminController
    {
        #region Fields

        private readonly IWithdrawRequestService _withdrawRequestService;
        private readonly IBankAccountCardService _bankAccountCardService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public WithdrawRequestController(IWithdrawRequestService withdrawRequestService, IStringLocalizer<SharedResource> localizer, IBankAccountCardService bankAccountCardService = null)
        {
            _withdrawRequestService = withdrawRequestService;
            _localizer = localizer;
            _bankAccountCardService = bankAccountCardService;
        }

        #endregion

        #region Actions

        #region List

        [Permission("ListOfWithdrawRequests")]
        public async Task<IActionResult> List(FilterWithdrawRequestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _withdrawRequestService.FilterWithdrawRequestAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return View(result.Value);
        }

        #endregion

        #region Accept

        [Permission("AcceptWithdrawRequest")]
        public async Task<IActionResult> Accept(AcceptWithdrawRequestViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values
                        .First(v => v.ValidationState is ModelValidationState.Invalid)
                        .Errors
                        .First()
                        .ErrorMessage;

                TempData[ErrorMessage] = message;

                return RedirectToAction(nameof(List));
            }

            var result = await _withdrawRequestService.AcceptAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            }

            if (returnUrl != null) return Redirect(returnUrl);

            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Reject

        [Permission("RejectWithdrawRequest")]
        public async Task<IActionResult> Reject(RejectWithdrawRequestViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values
                        .First(v => v.ValidationState is ModelValidationState.Invalid)
                        .Errors
                        .First()
                        .ErrorMessage;

                TempData[ErrorMessage] = message;

                return RedirectToAction(nameof(List));
            }

            var result = await _withdrawRequestService.RejectAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            }

            if (returnUrl != null) return Redirect(returnUrl);

            return RedirectToAction(nameof(List));
        }

        #endregion

        #region GetBankAccountCard

        [HttpGet("/admin/getbankaccountcard/{bankAccountCardId}")]
        public async Task<IActionResult> GetBankAccountCard(int bankAccountCardId)
        {
            var result = await _bankAccountCardService.GetByIdAsync(bankAccountCardId);

            if (result.IsSuccess)
            {
                return PartialView("_BankAccountCard", result.Value);
            }
            else
            {
                return PartialView("_BankAccountCard");
            }
        }

        #endregion

        #endregion
    }
}
