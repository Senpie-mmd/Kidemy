using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserRegisteredEvent(
        int Id,
        string Mobile,
        string Password
        ) : INotification;
}
