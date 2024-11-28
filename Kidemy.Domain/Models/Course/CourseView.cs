using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Course
{
    public class CourseView : BaseEntity<int>
    {
        public int CourseId { get; set; }
        public string UserIp { get; set; }
        public int? UserId { get; set; }

        #region Relations 
        public Course Course { get; set; }
        #endregion 
    }
}
