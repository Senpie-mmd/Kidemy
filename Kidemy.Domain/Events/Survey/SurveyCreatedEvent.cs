using MediatR;

namespace Kidemy.Domain.Events.Survey
{
    public record SurveyCreatedEvent(
        string Title,
        bool IsPublished
        ) : INotification;
}
