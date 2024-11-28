using MediatR;

namespace Kidemy.Domain.Events.Discount
{
    public record DiscountDeletedEvent(int Id) : INotification;

}
