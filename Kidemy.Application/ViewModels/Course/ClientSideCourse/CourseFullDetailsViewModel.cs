using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse
{
    public class CourseFullDetailsViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = "Course")]
        public string Title { get; set; }

        [Translate(EntityName = "Course")]
        public string ShortDescription { get; set; }

        [Translate(EntityName = "Course")]
        public string Description { get; set; }

        public string Slug { get; set; }
        public CourseLevel Level { get; set; }
        public CourseStatus Status { get; set; }
        public bool IsOffer { get; set; }
        public int MasterId { get; set; }
        public decimal? Price { get; set; }
        public TimeSpan VideosTotalTime { get; set; }
        public bool HasCertificate { get; set; }
        public bool AllowComenting { get; set; }
        public CourseType Type { get; set; }
        public bool IsPublished { get; set; }
        public string ImageName { get; set; }
        public string? DemoVideoFileName { get; set; }

        public List<CourseCategoryFullDetailsViewModel> Categories { get; set; }
    }
}
