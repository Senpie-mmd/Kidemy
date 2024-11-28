using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Shared;
using Kidemy.Web.Builders;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
	public class CartController : BaseController
	{
		#region Fields

		private readonly ICartService _cartService;
		private readonly IStringLocalizer<SharedResource> _localizer;
		private readonly IWalletService _walletService;

		#endregion

		#region Constructor

		public CartController(ICartService cartService, IStringLocalizer<SharedResource> localizer, IWalletService walletService)
		{
			_cartService = cartService;
			_localizer = localizer;
			_walletService = walletService;
		}

		#endregion

		#region Actions

		[HttpGet("/Cart")]
		[Authorize]
		public async Task<IActionResult> Index(string? discountCode = null)
		{
			var userId = User.GetUserId();

			var result = await _cartService.GetCurrentCartAsync(userId, discountCode);

			if (result.IsFailure)
			{
				TempData[ErrorMessage] = _localizer[result.Message].ToString();
				return Redirect("/");
			}

			if(result.Value.Items?.Count == 0)
			{
				TempData[ErrorMessage] = _localizer[ErrorMessages.YourCartIsEmptyError].ToString();
                return Redirect("/");
            }

			var balanceResult = await _walletService.GetBalanceAsync(userId);
			if (balanceResult.IsSuccess)
			{
				ViewData["WalletBalance"] = balanceResult.Value;
			}

            var appliedBlockAmountResult = await _walletService.GetBalanceAsync(userId, true);
            if (appliedBlockAmountResult.IsSuccess)
            {
                ViewData["BalanceWithAppliedBlockAmount"] = appliedBlockAmountResult.Value;
            }

            var discountCodeIsApplied = false;
			if (discountCode != null)
			{
				if (result.Value.AppliedDiscount?.Code == discountCode)
				{
					discountCodeIsApplied = true;
				}

				if (result.Value.Items?.Any(item => item.Course.AppliedDiscount?.Code == discountCode) ?? false)
				{
					discountCodeIsApplied = true;
				}
			}

			if (discountCodeIsApplied)
			{
				TempData[SuccessMessage] = _localizer[SuccessMessages.DiscountCodeAppliedSuccessfully].ToString();
			}
			else if (discountCode != null && !discountCodeIsApplied)
			{
				TempData[ErrorMessage] = _localizer[ErrorMessages.DiscountCodeIsNotValidError].ToString();
			}

			return View(result.Value);
		}

		[HttpGet("/GetCart")]
		public async Task<ResponseResult> GetCart()
		{
			if(!(User?.Identity?.IsAuthenticated ?? false))
			{
				return ResponseResult.Failure(_localizer[ErrorMessages.FirstLoginError].ToString());
			}

			var userId = User.GetUserId();

			var result = await _cartService.GetCurrentCartAsync(userId);

			if (result.IsSuccess)
			{
				return ResponseResult.Success(string.Empty, result.Value);
			}
			else
			{
				return ResponseResult.Failure(_localizer[result.Message].ToString());
			}
		}

		[HttpPost("/AddToCart")]
		public async Task<ResponseResult> AddToCart([FromBody] AddToCartViewModel model)
		{
            if (!(User?.Identity?.IsAuthenticated ?? false))
            {
                return ResponseResult.Failure(_localizer[ErrorMessages.FirstLoginError].ToString());
            }

            if (!ModelState.IsValid)
			{
				return ResponseResult.Failure(_localizer["InvalidData"].ToString());
			}

			var userId = User.GetUserId();

			model.UserId = userId;
			var result = await _cartService.AddItemToCurrentCart(model);

			if (result.IsFailure)
			{
				return ResponseResult.Failure(_localizer[result.Message].ToString());
			}
			else
			{
				return ResponseResult.Success(_localizer[result.Message].ToString());
			}
		}

		[HttpPost("/RemoveFromCart")]
		public async Task<ResponseResult> RemoveFromCart([FromBody] RemoveFromCartViewModel model)
		{
            if (!(User?.Identity?.IsAuthenticated ?? false))
            {
                return ResponseResult.Failure(_localizer[ErrorMessages.FirstLoginError].ToString());
            }

            if (!ModelState.IsValid) return ResponseResult.Failure(_localizer["InvalidData"].ToString());

			var userId = User.GetUserId();

			model.UserId = userId;
			var result = await _cartService.RemoveItemFromCurrentCart(model);

			if (result.IsFailure)
			{
				return ResponseResult.Failure(_localizer[result.Message].ToString());
			}
			else
			{
				return ResponseResult.Success(_localizer[result.Message].ToString());
			}
		}

		[HttpPost("/process-cart-payment")]
		[Authorize]
		public async Task<IActionResult> ProcessCartPayment(ProcessCartPaymentViewModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData[ErrorMessage] = _localizer["InvalidData"].ToString();
				return Redirect("/cart");
			}

			var userId = HttpContext.User.GetUserId();
			model.UserId = userId;
			model.UserIP = HttpContext.GetUserIP();

			var result = await _cartService.ProcessCartPayment(model);

			if (result.IsFailure)
			{
				TempData[ErrorMessage] = _localizer[result.Message].ToString();
				return Redirect("/");
			}

            var transaction = result.Value;

            var paymentGetwayResult = await PaymentBuilder.BuildActivePaymentController(HttpContext);

            if (paymentGetwayResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[paymentGetwayResult.Message].ToString();
                return View(model);
            }

            // order payment is completed by wallet balance and regitered a withdraw transaction
            if (result.Value.TransactionType == WalletTransactionType.Withdraw)
			{
				return paymentGetwayResult.Value.SuccessPayment(transaction);
			}

            return await paymentGetwayResult.Value.StartChargeWalletPayment(transaction.Id);

        }

        #endregion
    }
}
