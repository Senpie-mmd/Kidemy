using Kidemy.Application.ViewModels.Discount;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Statics;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public class ClientSideCourseShortDetailsViewModel : DiscountableViewModel<int>
    {
        public ClientSideCourseShortDetailsViewModel()
        {
            ImageFileName = SiteTools.DefaultImageName;
        }

        public string ImageFileName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CourseLevel Level { get; set; }
        public CourseStatus Status { get; set; }
        public int MasterId { get; set; }
        public decimal? Price { get; set; }
        public CourseType Type { get; set; }
        public string Slug { get; set; }

        public override decimal GetMainPrice()
        {
            return Price ?? 0;
        }
    }
}
