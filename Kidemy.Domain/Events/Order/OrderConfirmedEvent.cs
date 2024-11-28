using MediatR;

namespace Kidemy.Domain.Events.Order
{
    public record OrderConfirmedEvent(
            int Id
        ) : INotification;
}
