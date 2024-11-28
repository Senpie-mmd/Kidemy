using MediatR;

namespace Kidemy.Domain.Events.Cart
{
    public record CartItemAddedEvent(
            int CartId,
            int UserId,
            int CourseId
        ) : INotification;
}
