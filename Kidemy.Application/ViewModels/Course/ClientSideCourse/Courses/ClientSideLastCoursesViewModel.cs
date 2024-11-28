using Kidemy.Application.ViewModels.Discount;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public class ClientSideLastCoursesViewModel : DiscountableViewModel<int>
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string ImageFileName { get; set; }
        public decimal? Price { get; set; }
        public CourseType Type { get; set; }
        public double? AvrageScore { get; set; }
        public override decimal GetMainPrice()
        {
            return Price ?? 0;
        }
    }
}
