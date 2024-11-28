using MediatR;

namespace Kidemy.Domain.Events.Cart
{
    public record CartItemRemovedEvent(
            int CartId,
            int UserId,
            int CartItemId,
            int CourseId
        ) : INotification;
}
