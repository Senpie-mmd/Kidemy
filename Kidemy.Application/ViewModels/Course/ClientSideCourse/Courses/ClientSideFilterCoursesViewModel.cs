using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public class ClientSideFilterCoursesViewModel : BasePaging<ClientSideCourseViewModel>
    {
        public List<int>? CategoryIds { get; set; }
        public CourseLevel? Level { get; set; }
        public CoursePriceTypeForView? PriceType { get; set; } = 0;
        public FilterCoursesListEnum? CourseRates { get; set; } = 0;
        public string? Title { get; set; }
        public int AllCoursesCount { get; set; } = 0;
        public List<CategoriesForCourseListClientSideViewModel> categories { get; set; }
        public int? MasterId { get; set; }
        public string? CourseTag { get; set; }
    }
}
