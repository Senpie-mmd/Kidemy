using MediatR;

namespace Kidemy.Domain.Events.Course.CourseQuestion
{
    public record CourseQuestionConfirmedEvent(int id) : INotification;
}
