namespace Kidemy.Domain.Models.Course
{
    public class CourseCategoryMapping
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }

        #region Relations 
        public Course Course { get; set; }
        public CourseCategory Category { get; set; }
        #endregion
    }
}
