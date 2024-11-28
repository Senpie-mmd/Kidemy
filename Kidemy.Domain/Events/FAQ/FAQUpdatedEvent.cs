using MediatR;

namespace Kidemy.Domain.Events.FAQ
{
    public record FAQUpdatedEvent(
        int Id,
        string NewTitle,
        string OldTitle,
        string NewAnswer,
        string OldAnswer
        ) : INotification;

}
