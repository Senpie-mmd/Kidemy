using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Events.Cart;
using Kidemy.Domain.Interfaces.Cart;
using Kidemy.Domain.Models.Cart;
using Kidemy.Domain.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Services.Implementations
{
	public class CartService : ICartService
	{
		#region Fields

		private readonly ICartRepository _cartRepository;
		private readonly ICourseService _courseService;
		private readonly IDiscountService _discountService;
		private readonly ILocalizingService _localizingService;
		private readonly ILogger<CartService> _logger;
        private readonly IOrderService _orderService;
        private readonly IWalletService _walletService;
		private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor

        public CartService(ICartRepository cartRepository, ICourseService courseService, IDiscountService discountService, ILocalizingService localizingService, ILogger<CartService> logger, IOrderService orderService, IWalletService walletService, IMediatorHandler mediatorHandler)
        {
            _cartRepository = cartRepository;
            _courseService = courseService;
            _discountService = discountService;
            _localizingService = localizingService;
            _logger = logger;
            _orderService = orderService;
            _walletService = walletService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods

        private async Task<Cart> GetUserCart(int userId)
		{
			var cart = await _cartRepository.FirstOrDefaultAsync(c => c.UserId == userId, includeProperties: nameof(Cart.Items));

			if (cart is null)
			{
				cart = new Cart { UserId = userId };

				await _cartRepository.InsertAsync(cart);
				await _cartRepository.SaveAsync();
			}

			return cart;
		}

		public async Task<Result<CartViewModel>> GetCurrentCartAsync(int userId, string? discountCode = null)
		{
			if (userId <= 0) return Result.Failure<CartViewModel>(ErrorMessages.FailedOperationError);

			var cart = await GetUserCart(userId);

			var courseIds = cart.Items?.Select(item => item.CourseId)?.ToList();

			var coursesInCartResult = await _courseService.GetCoursesForClientByIdsAsync(courseIds);

			var coursesInCart = new List<ClientSideCourseShortDetailsViewModel>();

			if (coursesInCartResult.IsSuccess)
			{
				coursesInCart = coursesInCartResult.Value;
			}

			var cartViewModel = new CartViewModel().MapFrom(cart, coursesInCart);

            await _discountService.ApplyDiscount(cartViewModel, discountCode);

			if (cartViewModel.Items?.Any() ?? false)
			{
				await _localizingService.TranslateModelAsync(cartViewModel.Items.Select(item => item.Course).ToList());
			}

			return cartViewModel;
		}

		public async Task<Result<bool>> CanAddToCart(AddToCartViewModel model)
		{
			if (model is null) return Result.Failure<bool>(ErrorMessages.FailedOperationError);

			var cart = await GetUserCart(model.UserId);

			var expectedCartItem = cart.Items?.FirstOrDefault(item => item.CourseId == model.CourseId);

			if (expectedCartItem is not null)
			{
				return Result.Success(false, ErrorMessages.CourseAlreadyExistsInCartError);
			}

			var userHasCourseResult = await _courseService.UserHasCourseAsync(model.CourseId ?? 0, model.UserId);

			if (userHasCourseResult.IsFailure) return Result.Failure(false, userHasCourseResult.Message);

			if (userHasCourseResult.Value == true) return Result.Success(false, ErrorMessages.UserAlreadyBoughtCourseError);

			return Result.Success(true);
		}

		public async Task<Result> AddItemToCurrentCart(AddToCartViewModel model)
		{
			if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

			var cart = await GetUserCart(model.UserId);

			var canAddResult = await CanAddToCart(new AddToCartViewModel { CourseId = model.CourseId, UserId = model.UserId });

			if (canAddResult.IsFailure) return Result.Failure(canAddResult.Message);

			if (canAddResult.Value == false) return Result.Failure(canAddResult.Message);

			var courseResult = await _courseService.GetCourseForClientByIdAsync(model.CourseId ?? 0);

			if (courseResult.IsFailure) return Result.Failure(courseResult.Message);

			var course = courseResult.Value;

			var toAddCartItem = new CartItem
			{
				CartId = cart.Id,
				CourseId = course.Id
			};

			cart.Items ??= new List<CartItem>();
			cart.Items.Add(toAddCartItem);

			_cartRepository.Update(cart);
			await _cartRepository.SaveAsync();

			var @event = new CartItemAddedEvent(cart.Id, cart.UserId, course.Id);

			await _mediatorHandler.PublishEvent(@event);

			return Result.Success();
		}

		public async Task<Result> RemoveItemFromCurrentCart(RemoveFromCartViewModel model)
		{
			if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

			var cart = await GetUserCart(model.UserId);

			var expectedCartItem = cart.Items?.FirstOrDefault(item => item.Id == model.CartItemId);

			if (expectedCartItem is null) return Result.Failure(ErrorMessages.FailedOperationError);

			cart.Items.Remove(expectedCartItem);
			await _cartRepository.SaveAsync();

			var @event = new CartItemRemovedEvent(cart.Id, cart.UserId, expectedCartItem.Id, expectedCartItem.CourseId);

			await _mediatorHandler.PublishEvent(@event);

			return Result.Success();
		}

		public async Task<Result> ClearCurrentCartItems(int userId)
		{
            if (userId <= 0) return Result.Failure(ErrorMessages.FailedOperationError);

			await _cartRepository.ClearUserCartItems(userId);
            
            return Result.Success();
        }

		public async Task<Result<WalletTransactionDetailsViewModel>> ProcessCartPayment(ProcessCartPaymentViewModel model)
		{
			if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.FailedOperationError);

			var expectedCartResult = await GetCurrentCartAsync(model.UserId, model.DiscountCode);

			if (expectedCartResult.IsFailure) return Result.Failure<WalletTransactionDetailsViewModel>(expectedCartResult.Message);

			var currentCart = expectedCartResult.Value;

			if (currentCart is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.FailedOperationError);

			var registerOrderResult = await _orderService.RegisterOrderAsync(currentCart);

			if (registerOrderResult.IsFailure) return Result.Failure<WalletTransactionDetailsViewModel>(registerOrderResult.Message);

			var orderId = registerOrderResult.Value;
			await ClearCurrentCartItems(model.UserId);

			var chargeWalletAmount = currentCart.DiscountedTotalAmount ?? currentCart.TotalAmount;

			if (model.UseFromWalletBalance)
			{
				var walletBalanceResult = await _walletService.GetBalanceAsync(model.UserId, true);

				if (walletBalanceResult.IsFailure)
				{
					_logger.LogError($"ProcessCartPayment: GetBalanceAsync failed for user with id {model.UserId}");
				}
				else
				{
					chargeWalletAmount -= walletBalanceResult.Value;
					chargeWalletAmount = chargeWalletAmount > 0 ? chargeWalletAmount : 0;
				}
			}

			if (chargeWalletAmount == 0)
			{
				return await _orderService.ConfirmOrderPaymentAsync(orderId);
			}
			else
			{
				var chargeWalletModel = new ChargeWalletForOrderViewModel
				{
					OrderId = orderId,
					UserId = model.UserId,
					Amount = chargeWalletAmount,
					UserIP = model.UserIP
				};

				var chargeWalletResult = await _walletService.CreateChargeWalletTransactionForPayOrderAsync(chargeWalletModel);

				if (chargeWalletResult.IsFailure) return Result.Failure<WalletTransactionDetailsViewModel>(chargeWalletResult.Message);

				return chargeWalletResult.Value;
			}
		}

		#endregion
	}
}
