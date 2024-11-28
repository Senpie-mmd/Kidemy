using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.UserPanel
{
    public class UserPanelCourseViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(Course))]
        public string Title { get; set; }
        public int Episodes { get; set; }
        public CourseStatus Status { get; set; }
        public string Slug { get; set; }
        public string? ImageName { get; set; }
        public decimal? Price { get; set; }
    }
}
