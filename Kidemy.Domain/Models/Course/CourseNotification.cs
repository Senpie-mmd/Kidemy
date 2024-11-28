using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.CourseNotification;

namespace Kidemy.Domain.Models.Course
{
    public class CourseNotification : AuditBaseEntity<int>
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public CourseNotificationType CourseNotificationType { get; set; }
    }
}
