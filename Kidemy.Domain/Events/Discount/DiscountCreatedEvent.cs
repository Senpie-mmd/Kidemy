using Kidemy.Domain.Enums.Discount;
using MediatR;

namespace Kidemy.Domain.Events.Discount
{
    public record DiscountCreatedEvent(
            string Title,
            string Code,
            bool IsActive,
            DiscountType Type,
            bool AutoUse,
            DateTime? StartDateTimeOnUtc,
            DateTime? EndDateTimeOnUtc,
            bool IsPercentage,
            decimal Value
        ) : INotification;

}
