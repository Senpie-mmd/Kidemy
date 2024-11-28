using Kidemy.Domain.Enums.Discount;
using MediatR;

namespace Kidemy.Domain.Events.Discount
{
    public record DiscountLimitationCreatedEvent(
        int DiscountId,
        DiscountLimitationType Type,
        List<int>? Users,
        List<int>? Courses,
        List<int>? Categories
    ) : INotification;

}
