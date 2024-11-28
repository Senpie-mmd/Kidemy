using MediatR;

namespace Kidemy.Domain.Events.Course.CourseQuestion
{
    public record CourseQuestionDeletedEvent(int id) : INotification;
}
