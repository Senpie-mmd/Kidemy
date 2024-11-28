using MediatR;

namespace Kidemy.Domain.Events.Survey
{
    public record SurveyDeletedEvent(
        int Id
        ) : INotification;
}
