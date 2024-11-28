using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Course
{
    public class CourseTagMapping
    {
        public int CourseId { get; set; }
        public int TagId { get; set; }

        #region Relations
        public Course Course { get; set; }
        public CourseTag Tag { get; set; }
        #endregion
    }
}
