using MediatR;

namespace Kidemy.Domain.Events.Newsletter
{
    public record NewsletterCreatedEvent(
        string Email,
        string ip
        ) : INotification;

}
