using MediatR;

namespace Kidemy.Domain.Events.Course.CourseQuestionAnswer
{
    public record CourseQuestionAnswerDeletedEvent(int id) : INotification;
}
