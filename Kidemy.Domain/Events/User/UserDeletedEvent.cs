using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserDeletedEvent
        (
            int Id
        ) : INotification;

}
