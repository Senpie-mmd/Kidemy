using MediatR;
using System.Security;

namespace Kidemy.Domain.Events.Consultation.AdviserConsultationType
{
    public record AdviserConsultationTypeCreatedEvent(
        int id,
        int adviserId,
        string title,
        decimal price,
        bool isPublished
        ) : INotification;
}
