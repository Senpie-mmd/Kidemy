using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Course
{
    public class CourseCommentReport : AuditBaseEntity<int>
    {
        public int CommentId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public bool IsAdminChecked { get; set; }

        public int? CheckedById { get; set; }

        public CourseComment Comment { get; set; }
    }

}
