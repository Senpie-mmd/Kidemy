using Kidemy.Domain.Enums.Course;
using MediatR;

namespace Kidemy.Domain.Events.Course.Course
{
    public record CourseCreatedEvent(
        int Id,
        string Title,
        string ShortDescription,
        string Description,
        string Slug,
        CourseLevel Level,
        CourseStatus Status,
        bool IsOffer,
        int MasterId,
        bool HasCertificate,
        bool AllowCommenting,
        CourseType Type,
        bool IsPublshed,
        string ImageName,
        string DemoVideoFileName,
        List<string> Tags,
        List<int> CategoryIds
        ) : INotification;
}
