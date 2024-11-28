using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserPasswordUpdatedEvent
        (
            int Id,
            string PrevPassword,
            string NewPassword
        )
        : INotification;
}
