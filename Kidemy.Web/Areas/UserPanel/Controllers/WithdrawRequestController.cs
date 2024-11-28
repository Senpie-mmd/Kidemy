using Kidemy.Application.Services.Implementation;
using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.BankAccountCard;
using Kidemy.Application.ViewModels.WithrawRequest;
using Kidemy.Domain.Enums.BankAccount;
using Kidemy.Domain.Shared;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class WithdrawRequestController : BaseUserPanelController
    {
        #region Fields

        private readonly IWithdrawRequestService _withdrawRequestService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IBankAccountCardService _bankAccountCardService;
        private readonly ISiteSettingService _siteSettingService;
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;

        #endregion

        #region Constructor

        public WithdrawRequestController(IWithdrawRequestService withdrawRequestService, IStringLocalizer<SharedResource> localizer, IBankAccountCardService bankAccountCardService, ISiteSettingService siteSettingService, IUserService userService, IWalletService walletService)
        {
            _withdrawRequestService = withdrawRequestService;
            _localizer = localizer;
            _bankAccountCardService = bankAccountCardService;
            _siteSettingService = siteSettingService;
            _userService = userService;
            _walletService = walletService;
        }

        #endregion

        #region Actions

        #region List

        [HttpGet("/userpanel/withdraw-requests")]
        public async Task<IActionResult> List(FilterWithdrawRequestViewModel model)
        {
            model.UserId = User.GetUserId();
            var result = await _withdrawRequestService.FilterWithdrawRequestAsync(model);

            var widthrawResult = await _userService.MasterHasAccessToWithraw(model.UserId);

            if (widthrawResult.IsSuccess)
                ViewBag.HasAccessToWithdraw = widthrawResult.Value;

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(model);
        }

        #endregion

        #region Create

        [HttpGet("/userpanel/addwithdrawrequest")]
        public async Task<IActionResult> Create()
        {
            var filterBankAccountCard = new FilterBankAccountCardViewModel
            {
                UserId = User.GetUserId(),
                Status = BankAccountCardStatus.Accepeted
            };
            var bankAccounts = await _bankAccountCardService.FilterBankAccountCardAsync(filterBankAccountCard);

            var siteSetting = await _siteSettingService.GetSiteSettingAsync();

            if (bankAccounts.IsSuccess)
                ViewBag.BankAccounts = bankAccounts.Value;

            if (siteSetting.IsSuccess)
                ViewBag.MinimumWithdrawableAmount = siteSetting.Value.WithdrawRequestMinimumAmount;

            var balanceResult = await _walletService.GetBalanceAsync(User.GetUserId());
            if (balanceResult.IsSuccess)
            {
                ViewData["WalletBalance"] = balanceResult.Value;
            }

            var appliedBlockAmountResult = await _walletService.GetBalanceAsync(User.GetUserId(), true);
            if (appliedBlockAmountResult.IsSuccess)
            {
                ViewData["BalanceWithAppliedBlockAmount"] = appliedBlockAmountResult.Value;
            }

            return View(new UpsertWithdrawRequestViewModel());
        }

        [HttpPost("/userpanel/addwithdrawrequest")]
        public async Task<IActionResult> Create(UpsertWithdrawRequestViewModel model)
        {
            var balanceResult = await _walletService.GetBalanceAsync(User.GetUserId());
            if (balanceResult.IsSuccess)
            {
                ViewData["WalletBalance"] = balanceResult.Value;
            }

            var appliedBlockAmountResult = await _walletService.GetBalanceAsync(User.GetUserId(), true);
            if (appliedBlockAmountResult.IsSuccess)
            {
                ViewData["BalanceWithAppliedBlockAmount"] = appliedBlockAmountResult.Value;
            }

            if (!ModelState.IsValid) return View(model);
           
            model.UserId = User.GetUserId();

            var result = await _withdrawRequestService.CreateAsync(model);

            var filterBankAccountCard = new FilterBankAccountCardViewModel
            {
                UserId = User.GetUserId(),
                Status = BankAccountCardStatus.Accepeted
            };
            var bankAccounts = await _bankAccountCardService.FilterBankAccountCardAsync(filterBankAccountCard);

            var siteSetting = await _siteSettingService.GetSiteSettingAsync();

            if (bankAccounts.IsSuccess)
                ViewBag.BankAccounts = bankAccounts.Value;

            if (siteSetting.IsSuccess)
                ViewBag.MinimumWithdrawableAmount = siteSetting.Value.WithdrawRequestMinimumAmount;

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();

            return RedirectToAction(nameof(List));
        }

        #endregion

        #endregion
    }
}
