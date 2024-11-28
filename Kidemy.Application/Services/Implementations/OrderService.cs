using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Convertors;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Order;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Events.Order;
using Kidemy.Domain.Interfaces.Order;
using Kidemy.Domain.Models.Discount;
using Kidemy.Domain.Models.Order;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace Kidemy.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly IOrderRepository _orderRepository;
        private readonly IWalletService _walletService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILocalizingService _localizingService;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor

        public OrderService(IOrderRepository orderRepository, IWalletService walletService, IHttpContextAccessor httpContextAccessor, ILocalizingService localizingService, IUserService userService, ICourseService courseService, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _walletService = walletService;
            _httpContextAccessor = httpContextAccessor;
            _localizingService = localizingService;
            _userService = userService;
            _courseService = courseService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods

        public async Task<Result<int>> RegisterOrderAsync(CartViewModel cart)
        {
            if (cart == null) return Result.Failure<int>(ErrorMessages.FailedOperationError);

            if (cart.Items?.Count == 0) return Result.Failure<int>(ErrorMessages.YourCartIsEmptyError);

            var order = new Order
            {
                UserId = cart.UserId,
                TotalAmount = cart.TotalAmount,
                DiscountedTotalAmount = cart.DiscountedTotalAmount,
                Items = cart.Items.Select(item => new OrderItem
                {
                    UnitPrice = item.Course.Price ?? 0,
                    DiscountedUnitPrice = item.Course.DiscountedPrice,
                    CourseId = item.CourseId,
                }).ToList()
            };

            await _orderRepository.InsertAsync(order);
            await _orderRepository.SaveAsync();

            if (order.DiscountedTotalAmount != null && cart.AppliedDiscount != null)
            {
                order.DiscountUsage = new List<DiscountUsage>{
                    new DiscountUsage
                    {
                        DiscountId = cart.AppliedDiscount.Id,
                        ReducedAmount = order.TotalAmount - order.DiscountedTotalAmount.Value,
                        UsedByUserId = cart.UserId
                    }
                };
            }

            var index = 0;
            foreach (var item in order.Items)
            {
                if (item.DiscountedUnitPrice != null)
                {
                    item.DiscountUsage = new DiscountUsage
                    {
                        DiscountId = cart.Items[index].Course.AppliedDiscount.Id,
                        ReducedAmount = (item.UnitPrice - item.DiscountedUnitPrice.Value),
                        UsedByUserId = cart.UserId,
                        OrderItemId = item.Id,
                        OrderId = order.Id
                    };
                }

                index++;
            }

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync();

            var @event = new OrderRegisteredEvent(
                    order.Id,
                    order.UserId,
                    order.TotalAmount,
                    order.DiscountedTotalAmount,
                    order.Items.ToList(),
                    order.DiscountUsage?.FirstOrDefault(d => d.OrderItemId == null)?.DiscountId
                );

            await _mediatorHandler.PublishEvent(@event);

            return Result.Success(order.Id);
        }

        public async Task<Result<AdminSideFilterOrderViewModel>> FilterForAdminAsync(AdminSideFilterOrderViewModel filter)
        {
            if (filter is null)
                return Result.Failure<AdminSideFilterOrderViewModel>(ErrorMessages.FailedOperationError);

            var filterConditions = Filter.GenerateConditions<Order>();

            if (filter.UserId is not null && filter.UserId > 0)
            {
                filterConditions.Add(o => o.UserId == filter.UserId);
            }

            if (filter.FromDate != null)
            {
                filterConditions.Add(x => x.CreatedDateOnUtc >= filter.FromDate.ConvertToEnglishNumber().ParseUserDateToUTC());
            }

            if (filter.ToDate != null)
            {
                filterConditions.Add(x => x.CreatedDateOnUtc <= filter.ToDate.ConvertToEnglishNumber().ParseUserDateToUTC());
            }

            if (filter.IsPaid != null)
            {
                filterConditions.Add(x => x.IsPaid == filter.IsPaid);
            }

            if(filter.OrderId is not null)
            {
                filterConditions.Add(x => x.Id == filter.OrderId);
            }

            await _orderRepository.FilterAsync(filter, filterConditions, OrderMapper.MapAdminSideOrderViewModel);

            var fullNames = await _userService.GetUsersFullNames(filter.Entities.Select(u => u.UserId).ToList());

            foreach (var order in filter.Entities)
            {
                var fullName = fullNames.First(f => f.UserId == order.UserId);

                order.UserName = fullName.UserFullName;
            }

            return filter;
        }

        public async Task<Result<ClientSideFilterOrderViewModel>> FilterForClientAsync(ClientSideFilterOrderViewModel filter)
        {
            if (filter is null)
            {
                return Result.Failure<ClientSideFilterOrderViewModel>(ErrorMessages.FailedOperationError);
            }

            var filterConditions = Filter.GenerateConditions<Order>();

            if (filter.UserId is not null && filter.UserId > 0)
            {
                filterConditions.Add(o => o.UserId == filter.UserId);
            }

            await _orderRepository.FilterAsync(filter, filterConditions, OrderMapper.MapClientSideOrderViewModel);

            return filter;
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> ConfirmOrderPaymentAsync(int orderId)
        {
            if (orderId <= 0) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.FailedOperationError);

            var order = await _orderRepository.GetOrderWithRelations(orderId);

            if (order == null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.FailedOperationError);

            var withdrawModel = new WithdrawFromWalletViewModel
            {
                UserId = order.UserId,
                UserIP = _httpContextAccessor.HttpContext.GetUserIP(),
                Amount = order.DiscountedTotalAmount ?? order.TotalAmount,
                OrderId = orderId
            };

            var result = await _walletService.WithdrawFromWalletForOrderPaymentAsync(withdrawModel);

            if (result.IsFailure) return result;

            order.IsPaid = true;
            order.UpdatedDateOnUtc = DateTime.UtcNow;
            if (order.DiscountUsage?.Any(x => x.OrderItemId == null && x.OrderId == order.Id) ?? false)
            {
                order.DiscountUsage.First(x => x.OrderItemId == null && x.OrderId == order.Id).IsFinally = true;
                order.DiscountUsage.First(x => x.OrderItemId == null && x.OrderId == order.Id).UpdatedDateOnUtc = DateTime.UtcNow;
            }

            foreach (var item in order.Items)
            {
                if (item.DiscountUsage != null)
                {
                    item.DiscountUsage.IsFinally = true;
                    item.DiscountUsage.UpdatedDateOnUtc = DateTime.UtcNow;
                }
            }

            await _orderRepository.SaveAsync();

            var addCoursesForUserResult = await _courseService.AddCoursesForUser(order.UserId, order.Items.Select(item => item.CourseId).ToList());

            if (addCoursesForUserResult.IsFailure)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(addCoursesForUserResult.Message);
            }

            var @event = new OrderConfirmedEvent(order.Id);

            await _mediatorHandler.PublishEvent(@event);

            return result.Value;
        }

        public async Task<Result<ClientSideOrderDetailsViewModel>> GetOrderDetailsForClientAsync(int orderId, int userId)
        {
            if (orderId <= 0) return Result.Failure<ClientSideOrderDetailsViewModel>(ErrorMessages.FailedOperationError);

            var order = await _orderRepository
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId, includeProperties: nameof(Order.Items));

            if (order == null) return Result.Failure<ClientSideOrderDetailsViewModel>(ErrorMessages.FailedOperationError);

            var coursesInOrderResult = await _courseService.GetCoursesShortDetailsByIdsAsync(order.Items.Select(item => item.CourseId).ToList());

            var coursesInOrder = new List<CourseShortDetailsViewModel>();

            if (coursesInOrderResult.IsSuccess)
            {
                coursesInOrder = coursesInOrderResult.Value;
            }

            var model = new ClientSideOrderDetailsViewModel().MapFrom(order, coursesInOrder);

            return model;
        }

        public async Task<Result<AdminSideOrderDetailsViewModel>> GetOrderDetailsForAdminAsync(int orderId)
        {
            if (orderId <= 0) return Result.Failure<AdminSideOrderDetailsViewModel>(ErrorMessages.FailedOperationError);

            var order = await _orderRepository
                .FirstOrDefaultAsync(o => o.Id == orderId, includeProperties: nameof(Order.Items));

            if (order == null) return Result.Failure<AdminSideOrderDetailsViewModel>(ErrorMessages.FailedOperationError);

            var coursesInOrderResult = await _courseService.GetCoursesShortDetailsByIdsAsync(order.Items.Select(item => item.CourseId).ToList());

            var coursesInOrder = new List<CourseShortDetailsViewModel>();

            if (coursesInOrderResult.IsSuccess)
            {
                coursesInOrder = coursesInOrderResult.Value;
            }

            var userFullNameResult = await _userService.GetUserFullName(order.UserId);

            var userName = userFullNameResult.IsSuccess ? userFullNameResult.Value : "-";

            var model = new AdminSideOrderDetailsViewModel().MapFrom(order, coursesInOrder, userName);

            return model;
        }

        #endregion
    }
}
