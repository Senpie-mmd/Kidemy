using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserEmailActivatedEvent(int Id, string Email) : INotification;
}
