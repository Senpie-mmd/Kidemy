using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageWallet")]
    public class WalletController : BaseAdminController
    {
        #region Fields

        private readonly IWalletService _walletService;
        private readonly IStringLocalizer _localizer;

        #endregion

        #region Constructor

        public WalletController(IWalletService walletService,
            IStringLocalizer localizer)
        {
            _walletService = walletService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("ChargeWallet")]
        public async Task<IActionResult> ChargeWallet(ChargeWalletViewModel chargeWalletModel)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values
                        .First(v => v.ValidationState is ModelValidationState.Invalid)
                        .Errors
                        .First()
                        .ErrorMessage;

                TempData[ErrorMessage] = _localizer[message];
                return RedirectToAction("Profile", "User", new { id = chargeWalletModel.UserId });
            }

            if (string.IsNullOrWhiteSpace(chargeWalletModel.Description))
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.RequiredDescriptionError].ToString();
                return RedirectToAction("Profile", "User", new { id = chargeWalletModel.UserId });
            }

            chargeWalletModel.UserIP = HttpContext.GetUserIP();
            var result = await _walletService.ChargeWalletByAdminAsync(chargeWalletModel);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            }

            return RedirectToAction("Profile", "User", new { id = chargeWalletModel.UserId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("WithdrawFromWallet")]
        public async Task<IActionResult> WithdrawFromWallet(WithdrawFromWalletViewModel withdrawFromWalletModel)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values
                        .First(v => v.ValidationState is ModelValidationState.Invalid)
                        .Errors
                        .First()
                        .ErrorMessage;

                TempData[ErrorMessage] = _localizer[message];
                return RedirectToAction("Profile", "User", new { id = withdrawFromWalletModel.UserId });
            }

            if (string.IsNullOrWhiteSpace(withdrawFromWalletModel.Description))
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.RequiredDescriptionError].ToString();
                return RedirectToAction("Profile", "User", new { id = withdrawFromWalletModel.UserId });
            }

            withdrawFromWalletModel.UserIP = HttpContext.GetUserIP();
            var result = await _walletService.WithdrawFromWalletByAdminAsync(withdrawFromWalletModel);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            }

            return RedirectToAction("Profile", "User", new { id = withdrawFromWalletModel.UserId });
        }

        #endregion
    }
}
