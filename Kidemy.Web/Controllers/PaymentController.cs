using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Consultation.ConsultationRequest;
using Kidemy.Application.ViewModels.VIPMembers;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Shared;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public abstract class PaymentController : BaseController
    {
        public abstract Task<IActionResult> StartChargeWalletPayment(int transactionId);
        public abstract Task<IActionResult> StartBuyVIPPlanPayment(int transactionId);
        public abstract Task<IActionResult> StartBuyConsultationRequestPayment(int transactionId);

        public async Task<IActionResult> ErrorPayment(WalletTransactionDetailsViewModel transaction, string? refId = null)
        {
            var _walletService = Request.HttpContext.RequestServices.GetRequiredService<IWalletService>();
            await _walletService.SetReferenceIdForTransaction(transaction.Id, refId);

            transaction.RefId = refId;
            return View("ErrorPayment", transaction);
        }

        public IActionResult SuccessPayment(WalletTransactionDetailsViewModel transaction, string? refId = null)
        {
            transaction.RefId = refId;
            return View("SuccessPayment", transaction);
        }
    }

    public abstract class PaymentController<TCallBackRequestModel> : PaymentController
    {
        private protected static object _locker = new object();

        public abstract Task<IActionResult> CallBack(TCallBackRequestModel metaData);

        public abstract Result VerifyPaymentByGateway(TCallBackRequestModel metaData);

        public virtual async Task<IActionResult> ConfirmPayment(TCallBackRequestModel metaData, WalletTransactionDetailsViewModel transaction, string? refId = "")
        {
            var _walletService = HttpContext.RequestServices.GetRequiredService<IWalletService>();
            var _vIPPlanService = HttpContext.RequestServices.GetRequiredService<IVIPPlanService>();
            var _consultationRequest = HttpContext.RequestServices.GetRequiredService<IConsultationRequestService>();
            var _localizer = HttpContext.RequestServices.GetRequiredService<IStringLocalizer<SharedResource>>();

            Result confirmPaidTransactionResult;

            lock (_locker)
            {
                confirmPaidTransactionResult = VerifyPaymentByGateway(metaData);
                if (confirmPaidTransactionResult.IsSuccess)
                {
                    confirmPaidTransactionResult = _walletService.ConfirmPaidTransactionAsync(transaction.Id, refId).Result;
                }

                if (confirmPaidTransactionResult.IsSuccess && transaction.TransactionCase == WalletTransactionCase.VIPPlan)
                {
                    ClientSideConfirmPlanViewModel confirmPlanViewModel = new()
                    {
                        PlanId = transaction.PlanId.Value,
                        UserId = transaction.UserId,
                        UserIp = transaction.IP,
                    };

                    confirmPaidTransactionResult = _vIPPlanService.ConfirmPlanForUser(confirmPlanViewModel).Result;

                    TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyJoinVIPMember].ToString();
                }

                if (confirmPaidTransactionResult.IsSuccess && transaction.TransactionCase == WalletTransactionCase.ConsultationRequest)
                {
                    ClientSideConfirmConsultationRequestViewModel clientSideConfirmConsultation = new()
                    {
                        ConsultationRequestId = transaction.ConsulationRequestId.Value,
                        UserId = transaction.UserId,
                        UserIp = transaction.IP,
                    };

                    confirmPaidTransactionResult = _consultationRequest.ConfirmConsultationRequestForUser(clientSideConfirmConsultation).Result;

                    TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullPayConsultationRequest].ToString();
                }

                if (confirmPaidTransactionResult.IsSuccess && transaction.TransactionCase == WalletTransactionCase.PayOrder)
                {
                    var orderService = HttpContext.RequestServices.GetRequiredService<IOrderService>();

                    confirmPaidTransactionResult = orderService.ConfirmOrderPaymentAsync(transaction.OrderId.Value).Result;
                }

            }

            if (confirmPaidTransactionResult.IsFailure)
            {
                return await ErrorPayment(transaction, refId);
            }

            return SuccessPayment(transaction, refId);
        }
    }
}
