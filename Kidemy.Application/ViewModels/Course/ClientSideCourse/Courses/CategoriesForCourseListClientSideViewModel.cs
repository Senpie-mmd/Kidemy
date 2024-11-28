using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public class CategoriesForCourseListClientSideViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(Course))]
        public string Title { get; set; }
        public int CourseCount { get; set; }
        public int? ParentId { get; set; }
    }
}
