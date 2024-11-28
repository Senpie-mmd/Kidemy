using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public class ClientSideOfferedCoursesViewModel : BaseEntityViewModel<int>
    {
        public string CourseImage { get; set; }
        public int MasterId { get; set; }
        public string MasterFullName { get; set; }
        public string MasterAvatarFileName { get; set; }
        public string CourseTitle { get; set; }
        public decimal? CoursePrice { get; set; }
        public CourseType CourseType { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public string? ImageName { get; set; }
    }
}
