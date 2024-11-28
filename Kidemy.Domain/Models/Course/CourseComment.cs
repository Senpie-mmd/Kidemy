using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Domain.Models.Course
{
    public class CourseComment : AuditBaseEntity<int>
    {
        public string Message { get; set; }

        public CourseCommentsScore Score { get; set; }

        public bool IsConfirmed { get; set; }

        public int CommentedById { get; set; }

        public int CourseId { get; set; }

        public int? ReplyCommnetId { get; set; }

        #region Relations
        public Course Course { get; set; }

        public CourseComment? ReplyComment { get; set; }

        public ICollection<CourseComment> RepliedComments { get; set; }

        public ICollection<CourseCommentReport> CommentReports { get; set; }
        #endregion
    }

}
