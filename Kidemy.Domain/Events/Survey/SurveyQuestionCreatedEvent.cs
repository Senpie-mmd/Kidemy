using Kidemy.Domain.Enums.Survey;
using MediatR;

namespace Kidemy.Domain.Events.Survey
{
    public record SurveyQuestionCreatedEvent(
        string Title,
        SurveyQuestionType QuestionType,
        int SurveyId,
        int Priority,
        string? Options
        ) : INotification;
}
