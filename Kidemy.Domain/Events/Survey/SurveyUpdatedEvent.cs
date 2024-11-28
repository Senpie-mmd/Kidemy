using MediatR;

namespace Kidemy.Domain.Events.Survey
{
    public record SurveyUpdatedEvent(
        int Id,
        string prevTitle,
        bool prevIsPublished,
        string newTitle,
        bool newIsPublished
        ) : INotification;
}
