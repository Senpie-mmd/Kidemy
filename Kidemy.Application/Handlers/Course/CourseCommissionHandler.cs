using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Events.Order;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Handlers.Course
{
    public class CourseCommissionHandler : INotificationHandler<OrderConfirmedEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseCommissionHandler> _logger;
        private readonly IWalletService _walletService;

        public CourseCommissionHandler(IOrderService orderService, ILogger<CourseCommissionHandler> logger, ICourseService courseService, IWalletService walletService)
        {
            _orderService = orderService;
            _logger = logger;
            _courseService = courseService;
            _walletService = walletService;
        }

        public async Task Handle(OrderConfirmedEvent notification, CancellationToken cancellationToken)
        {
            var orderResult = await _orderService.GetOrderDetailsForAdminAsync(notification.Id);

            if (orderResult.IsFailure)
            {
                _logger.LogError("CourseCommissionHandler: Handle: get order failed by message: " + orderResult.Message);
                return;
            }

            var order = orderResult.Value;

            var boughtCoursesIds = order.Items.Select(item => item.CourseId).ToList();

            var courseMastersResult = await _courseService.GetMastersOfCourses(boughtCoursesIds);

            if (courseMastersResult.IsFailure)
            {
                _logger.LogError("CourseCommissionHandler: Handle: GetMastersOfCourses failed by message: " + courseMastersResult.Message);
                return;
            }

            var courseMasters = courseMastersResult.Value;

            foreach (var item in order.Items)
            {
                var master = courseMasters.FirstOrDefault(c => c.CourseId == item.CourseId);

                if (master is null)
                {
                    _logger.LogError($"CourseCommissionHandler: Handle: not found master for course by id : {item.CourseId}");
                }

                var commissionAmount = 0m;

                var paidAmountForItem = item.DiscountedUnitPrice ?? item.UnitPrice;
                if(paidAmountForItem > 0 && master.CommissionPercentage > 0)
                {
                    commissionAmount = paidAmountForItem * master.CommissionPercentage / 100;
                }

                if(commissionAmount > 0)
                {
                    var chargeResult = await _walletService.CreateChargeWalletTransactionForCourseCommissionAsync(new ChargeWalletForCommissionViewModel
                    {
                        Amount = commissionAmount,
                        CourseId = item.CourseId,
                        UserId = master.MasterId
                    });

                    if (chargeResult.IsFailure)
                    {
                        _logger.LogError($"CourseCommissionHandler: Handle: CreateChargeWalletTransactionForCourseCommissionAsync for master by id {master.MasterId} and course id : {item.CourseId} failed by message: " + chargeResult.Message);
                    }
                }
            }
        }
    }
}
