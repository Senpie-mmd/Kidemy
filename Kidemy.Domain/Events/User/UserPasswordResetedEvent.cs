using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserPasswordResetedEvent
        (
            int Id,
            string PrevPassword,
            string NewPassword
        )
        : INotification;
}
