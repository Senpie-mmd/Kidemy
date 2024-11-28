using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Course
{
    public class CourseTag : BaseEntity<int>
    {
        public string Title { get; set; }

        #region Relations 
        public ICollection<CourseTagMapping> Courses { get; set; }
        #endregion
    }
}
