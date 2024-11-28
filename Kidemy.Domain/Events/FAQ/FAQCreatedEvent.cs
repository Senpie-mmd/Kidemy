using MediatR;

namespace Kidemy.Domain.Events.FAQ
{
    public record FAQCreatedEvent(
        string Title,
        string Answer
        ) : INotification;

}
