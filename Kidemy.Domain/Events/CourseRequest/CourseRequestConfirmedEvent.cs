using MediatR;

namespace Kidemy.Domain.Events.Course.CourseQuestion
{
    public record CourseRequestConfirmedEvent(int id) : INotification;
}
