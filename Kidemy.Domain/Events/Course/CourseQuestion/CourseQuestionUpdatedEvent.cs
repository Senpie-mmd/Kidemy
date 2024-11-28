using MediatR;

namespace Kidemy.Domain.Events.Course.CourseQuestion
{
    public record CourseQuestionUpdatedEvent(
        int courseQuestionId,
        string oldTitle,
        string newTitle,
        string oldDescription,
        string newDescription,
        int askedById,
        int courseId,
        bool oldIsClosed,
        bool newIsClosed
        ) : INotification;

}
