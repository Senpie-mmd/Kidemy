using Dto.Payment;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.ZarinPalPayment;
using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using ZarinPal.Class;

namespace Kidemy.Web.Controllers
{
    public class ZarinPalPaymentController : PaymentController<ZarinpalCallbackRequestModel>
    {
        #region Fields

        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IWalletService _walletService;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ZarinPalPaymentController> _logger;

        #endregion

        #region Constructor

        public ZarinPalPaymentController(
            IStringLocalizer<SharedResource> localizer,
            IWalletService walletService,
            ILogger<ZarinPalPaymentController> logger,
            LinkGenerator linkGenerator)
        {
            _localizer = localizer;
            _walletService = walletService;
            _logger = logger;
            _linkGenerator = linkGenerator;
        }

        #endregion

        #region Actions

        public override async Task<IActionResult> StartChargeWalletPayment(int transactionId)
        {
            var transactionResult = await _walletService.GetWalletTransactionByIdAsync(transactionId);

            if (transactionResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[transactionResult.Message];
                return Redirect("/");
            }

            var transaction = transactionResult.Value;

            var callbackUrlZarinPal = SiteTools.SiteAddress + _linkGenerator.GetPathByName("ZarinPalCallBack",
                new
                {
                    id = transaction.Id,
                    amount = (int)transaction.Amount,
                    userId = transaction.UserId
                });

            var expose = new Expose();
            var _payment = expose.CreatePayment();

            var result = await _payment.Request(new DtoRequest()
            {
                Mobile = HttpContext.User.GetMobile(),
                CallbackUrl = callbackUrlZarinPal,
                Description = transaction.TransactionCase == WalletTransactionCase.ChargeWallet ? _localizer["ChargeWallet"].ToString() : _localizer["PayOrder"].ToString(),
                Email = HttpContext.User.GetEmail(),
                Amount = (int)transaction.Amount,
                MerchantId = SiteTools.ZarinPalMerchantId
            }, SiteTools.IsZarinPalSandBox ? Payment.Mode.sandbox : Payment.Mode.zarinpal);

            if (result?.Status == (int)ZarinPalPaymentStatus.St100)
            {
                return Redirect($"{SiteTools.ZarinPalPaymentAddress}{result.Authority}");
            }
            else
            {
                TempData[ErrorMessage] = (string)_localizer[ErrorMessages.ZarinpalCreatePaymentRequestFailedError];
                return Redirect("/");
            }
        }

        public override async Task<IActionResult> StartBuyVIPPlanPayment(int transactionId)
        {
            var transactionResult = await _walletService.GetWalletTransactionByIdAsync(transactionId);

            if (transactionResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[transactionResult.Message];
                return Redirect("/");
            }

            var transaction = transactionResult.Value;

            var callbackUrlZarinPal = SiteTools.SiteAddress + _linkGenerator.GetPathByName("ZarinPalCallBack",
                new
                {
                    id = transaction.Id,
                    amount = (int)transaction.Amount,
                    userId = transaction.UserId
                });

            var expose = new Expose();
            var _payment = expose.CreatePayment();

            var result = await _payment.Request(new DtoRequest()
            {
                Mobile = HttpContext.User.GetMobile(),
                CallbackUrl = callbackUrlZarinPal,
                Description = transaction.TransactionCase == WalletTransactionCase.ChargeWallet ? _localizer["ChargeWallet"].ToString() : _localizer["PayOrder"].ToString(),
                Email = HttpContext.User.GetEmail(),
                Amount = (int)transaction.Amount,
                MerchantId = SiteTools.ZarinPalMerchantId
            }, SiteTools.IsZarinPalSandBox ? Payment.Mode.sandbox : Payment.Mode.zarinpal);

            if (result?.Status == (int)ZarinPalPaymentStatus.St100)
            {
                return Redirect($"{SiteTools.ZarinPalPaymentAddress}{result.Authority}");
            }
            else
            {
                TempData[ErrorMessage] = (string)_localizer[ErrorMessages.ZarinpalCreatePaymentRequestFailedError];
                return Redirect("/");
            }
        }

        public override async Task<IActionResult> StartBuyConsultationRequestPayment(int transactionId)
        {
            var transactionResult = await _walletService.GetWalletTransactionByIdAsync(transactionId);

            if (transactionResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[transactionResult.Message];
                return Redirect("/");
            }

            var transaction = transactionResult.Value;

            var callbackUrlZarinPal = SiteTools.SiteAddress + _linkGenerator.GetPathByName("ZarinPalCallBack",
                new
                {
                    id = transaction.Id,
                    amount = (int)transaction.Amount,
                    userId = transaction.UserId
                });

            var expose = new Expose();
            var _payment = expose.CreatePayment();

            var result = await _payment.Request(new DtoRequest()
            {
                Mobile = HttpContext.User.GetMobile(),
                CallbackUrl = callbackUrlZarinPal,
                Description = transaction.TransactionCase == WalletTransactionCase.ChargeWallet ? _localizer["ChargeWallet"].ToString() : _localizer["PayOrder"].ToString(),
                Email = HttpContext.User.GetEmail(),
                Amount = (int)transaction.Amount,
                MerchantId = SiteTools.ZarinPalMerchantId
            }, SiteTools.IsZarinPalSandBox ? Payment.Mode.sandbox : Payment.Mode.zarinpal);

            if (result?.Status == (int)ZarinPalPaymentStatus.St100)
            {
                return Redirect($"{SiteTools.ZarinPalPaymentAddress}{result.Authority}");
            }
            else
            {
                TempData[ErrorMessage] = (string)_localizer[ErrorMessages.ZarinpalCreatePaymentRequestFailedError];
                return Redirect("/");
            }
        }

        [HttpGet("zp-callback", Name = "ZarinPalCallBack")]
        public override async Task<IActionResult> CallBack(ZarinpalCallbackRequestModel metaData)
        {
            var transactionResult = await _walletService.GetWalletTransactionByIdAsync(metaData.Id);

            if (transactionResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[transactionResult.Message];
                return Redirect("/");
            }

            var transaction = transactionResult.Value;

            if (metaData.Status == "" || metaData.Status.ToString().ToLower() != "ok" || metaData.Authority == "")
            {
                _logger.LogError($"Payment of transaction by id ({transaction.Id}) failed.");

                return await ErrorPayment(transaction, metaData.Authority);
            }

            if (metaData.Authority.Length != 36) return await ErrorPayment(transaction, metaData.Authority);

            if (metaData.Status.ToLower() != "ok") return await ErrorPayment(transaction, metaData.Authority);

            if (transaction.Amount != metaData.Amount) return await ErrorPayment(transaction, metaData.Authority);

            if (transaction.UserId != metaData.UserId) return await ErrorPayment(transaction, metaData.Authority);

            return await ConfirmPayment(metaData, transaction, metaData.Authority);
        }

        public override Result VerifyPaymentByGateway(ZarinpalCallbackRequestModel metaData)
        {
            var expose = new Expose();
            var _payment = expose.CreatePayment();

            var verification = _payment.Verification(new DtoVerification
            {
                Amount = metaData.Amount,
                MerchantId = SiteTools.ZarinPalMerchantId,
                Authority = metaData.Authority
            }, SiteTools.IsZarinPalSandBox ? Payment.Mode.sandbox : Payment.Mode.zarinpal).Result;


            if (verification.Status != (int)ZarinPalPaymentStatus.St100)
            {
                _logger.LogError("VerifyPaymentByGetway: failed by response: " + JsonConvert.SerializeObject(verification));
                return Result.Failure(ErrorMessages.PaymentFailedError);
            }
            else
            {
                return Result.Success();
            }
        }

        #endregion
    }
}
