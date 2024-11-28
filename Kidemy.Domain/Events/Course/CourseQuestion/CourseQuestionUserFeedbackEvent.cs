using Kidemy.Domain.Enums.Course;
using MediatR;

namespace Kidemy.Domain.Events.Course.CourseQuestion
{
    public record CourseQuestionUserFeedbackEvent
    (UserReaction LastUserReaction,
        UserReaction CurrentUserReaction
        , bool IsClosed)
    : INotification;
}
