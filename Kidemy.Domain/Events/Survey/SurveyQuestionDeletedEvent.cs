using MediatR;

namespace Kidemy.Domain.Events.Survey
{
    public record SurveyQuestionDeletedEvent(
        int Id
        ) : INotification;
}
