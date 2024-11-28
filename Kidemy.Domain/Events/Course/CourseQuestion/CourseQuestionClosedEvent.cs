using MediatR;

namespace Kidemy.Domain.Events.Course.CourseQuestion
{
    public record CourseQuestionClosedEvent(int id) : INotification;
}
