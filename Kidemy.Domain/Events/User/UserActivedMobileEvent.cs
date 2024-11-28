using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserActivedMobileEvent
        (
            int Id,
            string Mobile
        )
        : INotification;

}
