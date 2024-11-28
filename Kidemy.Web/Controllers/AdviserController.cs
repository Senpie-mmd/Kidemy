using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Consultation.Adviser;
using Kidemy.Application.ViewModels.Consultation.ConsultationRequest;
using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Shared;
using Kidemy.Web.Builders;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class AdviserController : BaseController
    {

        #region Fields
        private readonly IAdviserSerivce _adviserSerivce;
        private readonly IConsultationRequestService _consultationRequestService;
        private readonly IWalletService _walletService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor
        public AdviserController(IAdviserSerivce adviserSerivce, IWalletService walletService, IConsultationRequestService consultationRequestService, IStringLocalizer<SharedResource> localizer)
        {
            _adviserSerivce = adviserSerivce;
            _walletService = walletService;
            _consultationRequestService = consultationRequestService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region Filter Advisers
        [HttpGet("/Advisers")]

        public async Task<IActionResult> FilterAdvisers(ClientSideFilterAdvisersViewModel model)
        {
            var advisers = await _adviserSerivce.ClientSideFilterAdvisers(model);
            if(advisers.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[advisers.Message].ToString();
                return RedirectToAction("Index", "Home");
            }
            return View(advisers.Value);
        }
        #endregion

        #region Get consultation Request

        [HttpGet("consultation-request/{consultationRequestId}")]
        public async Task<IActionResult> GetConsultationRequest(int consultationRequestId)
        {
            var result = await _consultationRequestService.ClientSideGetConsultaionRequest(consultationRequestId);

            if (result.IsFailure)
            {
                return NotFound();
            }

            var userId = HttpContext.User.GetUserId();

            var balanceResult = await _walletService.GetBalanceAsync(userId);

            if (balanceResult.IsSuccess)
            {
                ViewData["Balance"] = balanceResult.Value;
            }

            return View(result.Value);
        }

        #endregion

        #region BuyConsultationRequest

        [HttpPost("/buy-consultation-request"), ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyConsultationRequest(ClientSideBuyConsultationRequestViewModel model)
        {
            if (!ModelState.IsValid) return View();

            model.UserId = HttpContext.User.GetUserId();
            model.UserIP = HttpContext.GetUserIP();

            var result = await _consultationRequestService.BuyConsultationRequestAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
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

                return await paymentGetwayResult.Value.StartBuyConsultationRequestPayment(transaction.Id);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullPayConsultationRequest].ToString();
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            }

        }

        #endregion 

        #endregion

    }
}
