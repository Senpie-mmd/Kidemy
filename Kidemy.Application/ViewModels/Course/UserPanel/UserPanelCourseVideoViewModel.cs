using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Application.ViewModels.Course.UserPanel
{
    public class UserPanelCourseVideoViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(CourseVideo))]
        public string Title { get; set; }
        public bool IsPublished { get; set; }
        public bool IsFree { get; set; }
        public TimeSpan VideoLength { get; set; }
    }
}
