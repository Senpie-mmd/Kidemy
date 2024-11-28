using MediatR;

namespace Kidemy.Domain.Events.Course.CourseVideo
{
    public record CourseVideoCreatedEvent(
        int Id,
        string Title,
        int CourseId,
        bool IsFree,
        bool IsPublished,
        string ThumbnailImage,
        int Priority,
        string VideoFileName,
        TimeSpan VideoLength
        ) : INotification;
}
