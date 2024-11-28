using MediatR;

namespace Kidemy.Domain.Events.Consultation.AdviserAvailableDate
{
    public record AdviserAvailableDateCreatedEvent(
        int id,
        int adviserId,
        DayOfWeek dayOfWeek,
        TimeSpan startTime,
        TimeSpan endTime
        ) : INotification;


}
