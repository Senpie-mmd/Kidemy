using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion
{
    public class ClientSideFilterCourseQuestionViewModel : BasePaging<ClientSideCourseQuestionViewModel>
    {
        public string CourseSlug { get; set; }
        public string? CourseTitle { get; set; }

        public int AskedById { get; set; }

        public int TeacherId{ get; set; }

    }
}
