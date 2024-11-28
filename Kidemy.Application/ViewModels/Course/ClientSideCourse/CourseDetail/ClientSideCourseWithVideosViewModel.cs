using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail
{
    public class ClientSideCourseWithVideosViewModel : BaseEntityViewModel<int>
    {
        public ClientSideCourseWithVideosViewModel()
        {
            Videos = new List<ClientSideCourseVideoViewModel>();
        }

        public CourseType Type { get; set; }

        public List<ClientSideCourseVideoViewModel>? Videos { get; set; }
    }
}
