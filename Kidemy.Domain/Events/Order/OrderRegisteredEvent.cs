using Kidemy.Domain.Models.Order;
using MediatR;

namespace Kidemy.Domain.Events.Order
{
    public record OrderRegisteredEvent(
            int Id,
            int UserId,
            decimal TotalAmount,
            decimal? DiscountedTotalAmount,
            List<OrderItem> OrderItems,
            int? AppliedDiscountId
        ) : INotification;
}
