using MediatR;

namespace Kidemy.Domain.Events.Course.FavouriteCourse
{
    public record FavouriteCourseCreatedEvent(
        int userId,
        int courseId
        ) : INotification;
}
