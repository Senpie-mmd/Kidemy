using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public class ClientSideMainPagePopularCoursesViewModel : BaseEntityViewModel<int>
    {
        public string CategoryTitle { get; set; }

        public List<ClientSideCourseViewModel> PopularCourses { get; set; }
    }
}
