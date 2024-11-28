using MediatR;

namespace Kidemy.Domain.Events.Newsletter
{
    public record NewsletterDeletedEvent(
    int Id
    ) : INotification;
}
