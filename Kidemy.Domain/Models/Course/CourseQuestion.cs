using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Domain.Models.Course
{
    public class CourseQuestion : AuditBaseEntity<int>
    {
        #region Properties
        public string Title { get; set; }

        public string Description { get; set; }

        public int AskedById { get; set; }

        public int CourseId { get; set; }

        public bool IsClosed { get; set; }

        public bool IsConfirmed { get; set; }

        public UserReaction UserReaction { get; set; }

        #endregion

        #region Relations
        public ICollection<CourseQuestionAnswer> Answers { get; set; }
        public Course Course { get; set; }
        #endregion

    }

}
