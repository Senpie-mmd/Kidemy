using MediatR;

namespace Kidemy.Domain.Events.FAQ
{
    public record FAQDeletedEvent(
        int Id
        ) : INotification;
}
