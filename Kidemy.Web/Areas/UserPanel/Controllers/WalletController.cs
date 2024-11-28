using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Web.Builders;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class WalletController : BaseUserPanelController
    {
        #region Fields

        private readonly IWalletService _walletService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IMasterService _masterService;

        #endregion

        #region Constructor

        public WalletController(IWalletService walletService, IStringLocalizer<SharedResource> localizer, IMasterService masterService)
        {
            _walletService = walletService;
            _localizer = localizer;
            _masterService = masterService;
        }

        #endregion

        #region Actions

        #region Index

        [HttpGet("userpanel/wallet")]
        public async Task<IActionResult> Index()
        {
            var blockedAmount = await _masterService.GetBlockedBalanceAsync(User.GetUserId());

            if (blockedAmount.IsSuccess)
                ViewBag.MasterBlockedBalnace = blockedAmount.Value.BlockedAmount;

            return View();
        }

        [ActionName("Index")]
        [HttpPost("userpanel/wallet"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChargeWallet(ClientSideChargeWalletViewModel model)
        {
            var blockedAmount = await _masterService.GetBlockedBalanceAsync(User.GetUserId());

            if (blockedAmount.IsSuccess)
                ViewBag.MasterBlockedBalnace = blockedAmount.Value.BlockedAmount;

            if (!ModelState.IsValid) return View("Index", model);

            model.UserId = HttpContext.User.GetUserId();
            model.UserIP = HttpContext.GetUserIP();

            var result = await _walletService.CreateChargeWalletTransactionAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            var transaction = result.Value;

            var paymentGetwayResult = await PaymentBuilder.BuildActivePaymentController(HttpContext);

            if (paymentGetwayResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[paymentGetwayResult.Message].ToString();
                return View(model);
            }

            return await paymentGetwayResult.Value.StartChargeWalletPayment(transaction.Id);
        }

        #endregion

        #region TransactionsList

        [HttpGet("userpanel/transactions")]
        public async Task<IActionResult> TransactionsList(FilterWalletTransactionViewModel model)
        {
            model.UserId = User.GetUserId();
            var result = await _walletService.FilterAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(model);
        }

        #endregion

        #endregion
    }
}
