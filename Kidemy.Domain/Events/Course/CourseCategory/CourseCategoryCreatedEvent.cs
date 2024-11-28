using MediatR;

namespace Kidemy.Domain.Events.Course.CourseCategory
{
    public record CourseCategoryCreatedEvent(
        int Id,
        string Title,
        string LogoFileName
        ) : INotification;
}
