using MediatR;

namespace Kidemy.Domain.Events.Course.CourseCategory
{
    public record CourseCategoryUpdatedEvent(
        int Id,
        string prevTitle,
        string newTitle
        ) : INotification;
}
