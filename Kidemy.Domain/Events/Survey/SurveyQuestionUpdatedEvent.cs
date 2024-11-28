using Kidemy.Domain.Enums.Survey;
using MediatR;

namespace Kidemy.Domain.Events.Survey
{
    public record SurveyQuestionUpdatedEvent(
        int Id,
        string prevTitle,
        int prevSurveyId,
        SurveyQuestionType prevQuestionType,
        int prevPriority,
        string? prevOptions,
        string newTitle,
        int newSurveyId,
        SurveyQuestionType newQuestionType,
        int newPriority,
        string? newOptions
        ) : INotification;
}
