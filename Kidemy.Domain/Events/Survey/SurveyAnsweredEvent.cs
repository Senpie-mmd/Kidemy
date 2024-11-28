using MediatR;

namespace Kidemy.Domain.Events.Survey
{
    public record SurveyAnsweredEvent(
        int SurveyId,
        int AnswrerId,
        List<(int QuestionId, string AnswerText)> Answers) : INotification;
}
