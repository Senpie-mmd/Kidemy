using MediatR;

namespace Kidemy.Domain.Events.Course.CourseVideo
{
    public record CourseVideoUpdatedEvent(
        int Id,
        string prevTitle,
        int prevCourseId,
        bool prevIsFree,
        bool prevIsPublished,
        string prevThumbnailImage,
        int prevPriority,
        string prevVideoFileName,
        TimeSpan prevVideoLength,
        string newTitle,
        int newCourseId,
        bool newIsFree,
        bool newIsPublished,
        string newThumbnailImage,
        int newPriority,
        string newVideoFileName,
        TimeSpan newVideoLength
        ) : INotification;
}
