using Barnamenevisan.Localizing.Attributes;
using Kidemy.Application.ViewModels.Discount;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public class ClientSideCourseViewModel : DiscountableViewModel<int>
    {
        public string ImageFileName { get; set; }

        [Translate(EntityName = "Course")]
        public string Title { get; set; }

        [Translate(EntityName = "Course")]
        public string Description { get; set; }
        public CourseLevel Level { get; set; }
        public CourseStatus Status { get; set; }
        public int MasterId { get; set; }
        public decimal? Price { get; set; }
        public CourseType Type { get; set; }
        public string Slug { get; set; }
        public double? AvrageScore { get; set; }
        public int? VideosCount { get; set; } = 0;
        public List<TimeSpan>? CourseVideosLength { get; set; }

        public override decimal GetMainPrice()
        {
            return Price ?? 0;
        }
    }
}
