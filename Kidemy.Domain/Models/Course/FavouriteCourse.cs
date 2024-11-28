using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Course
{
    public class FavouriteCourse : AuditBaseEntity<int>
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
    }
}
