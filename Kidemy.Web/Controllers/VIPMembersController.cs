using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.VIPMembers;
using Kidemy.Application.ViewModels.VIPPlan;
using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Shared;
using Kidemy.Web.Builders;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class VIPMembersController : BaseController
    {
        #region Fields

        private readonly IVIPMembersService _vIPMembersService;
        private readonly IVIPPlanService _vIPPlanService;
        private readonly IDynamicTextService _dynamicTextService;
        private readonly IWalletService _walletService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public VIPMembersController(
            IVIPMembersService vIPMembersService,
            IVIPPlanService vIPPlanService,
            IDynamicTextService dynamicTextService,
            IStringLocalizer<SharedResource> localizer,
            IWalletService walletService)
        {
            _vIPMembersService = vIPMembersService;
            _vIPPlanService = vIPPlanService;
            _dynamicTextService = dynamicTextService;
            _localizer = localizer;
            _walletService = walletService;
        }

        #endregion

        #region Actions

        #region JoinVIPMembers

        [HttpGet("/join-VIP-members")]
        public IActionResult JoinVIPMembers()
        {
            if (User.Identity?.IsAuthenticated ?? false) return Redirect("/");

            return View();
        }


        [HttpPost("/join-VIP-members"), ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinVIPMembers(ClientSideJoinVIPMembersViewModel model)
        {
            if (User.Identity?.IsAuthenticated ?? false) return Redirect("/");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var joinResult = await _vIPMembersService.JoinVIPMembersAsync(model);

            if (joinResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[joinResult.Message].ToString();
                return View(model);
            }

            return View(model);
        }

        #endregion

        #region GetAllVIPPlans

        [HttpGet("VIP-plans")]
        public async Task<IActionResult> GetAllVIPPlans()
        {
            var result = await _vIPPlanService.GetAllVIPPlans();

            if (result.IsFailure)
            {
                return NotFound();
            }

            var vipPageText = await _dynamicTextService.GetDynamicTextByPosition(Domain.Enums.DynamicText.DynamicTextPosition.VIPPageText);
            if (vipPageText.IsSuccess)
            {
                ViewData["VipPageText"] = vipPageText.Value.Text;
            }

            var vipInvoiceText = await _dynamicTextService.GetDynamicTextByPosition(Domain.Enums.DynamicText.DynamicTextPosition.VIPInvoiceText);
            if (vipInvoiceText.IsSuccess)
            {
                ViewData["VipInvoiceText"] = vipInvoiceText.Value.Text;
            }

            var userId = HttpContext.User.GetUserId();

            var balanceResult = await _walletService.GetBalanceAsync(userId);
            if (balanceResult.IsSuccess)
            {
                ViewData["Balance"] = balanceResult.Value;
            }

            var appliedBlockAmountResult = await _walletService.GetBalanceAsync(userId, true);
            if (appliedBlockAmountResult.IsSuccess)
            {
                ViewData["BalanceWithAppliedBlockAmount"] = appliedBlockAmountResult.Value;
            }

            return View(result.Value);
        }

        #endregion

        #region BuyVIPPlan

        [HttpPost("/buy-VIP-plan"), ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyVIPPlan(ClientSideBuyVIPPlanViewModel model)
        {
            if (!ModelState.IsValid) return View();

            model.UserId = HttpContext.User.GetUserId();
            model.UserIP = HttpContext.GetUserIP();

            var result = await _vIPPlanService.BuyVIPPlanAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.YouAreVIPMemberError].ToString();
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            }

            var transaction = result.Value;

            if (transaction.TransactionType == WalletTransactionType.Deposit)
            {
                var paymentGetwayResult = await PaymentBuilder.BuildActivePaymentController(HttpContext);

                if (paymentGetwayResult.IsFailure)
                {
                    TempData[ErrorMessage] = _localizer[paymentGetwayResult.Message].ToString();
                    return View(model);
                }

                return await paymentGetwayResult.Value.StartBuyVIPPlanPayment(transaction.Id);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyJoinVIPMember].ToString();
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            }

        }

        #endregion 

        #endregion
    }
}
