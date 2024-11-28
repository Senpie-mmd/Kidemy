using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserMobileActivatedEvent(int Id, string Mobile) : INotification;
}
