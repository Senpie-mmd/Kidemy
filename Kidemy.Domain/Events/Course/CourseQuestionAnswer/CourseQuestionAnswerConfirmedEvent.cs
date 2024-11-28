using MediatR;

namespace Kidemy.Domain.Events.Course.CourseQuestionAnswer
{
    public record CourseQuestionAnswerConfirmedEvent(int id) : INotification;
}
