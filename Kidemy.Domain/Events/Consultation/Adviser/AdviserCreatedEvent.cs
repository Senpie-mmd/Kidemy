using MediatR;

namespace Kidemy.Domain.Events.Consultation.Adviser
{
    public record AdviserCreatedEvent(
        int id,
        bool isPublished,
        int consultationPercentage,
        int priority,
        int userId
        ) : INotification;
}
