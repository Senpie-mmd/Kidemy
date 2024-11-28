using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Course
{
    public class CourseVideoCategory : BaseEntity<int>
    {
        public string Title { get; set; }
        public int CourseId { get; set; }

        #region Relations
        public ICollection<CourseVideo> Videos { get; set; }
        #endregion
    }
}
