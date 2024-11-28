using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse
{
    public class CourseCategoryFullDetailsViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = "CourseCategory")]
        public string Title { get; set; }
        public int? ParentCourseCategoryId { get; set; }
    }
}
