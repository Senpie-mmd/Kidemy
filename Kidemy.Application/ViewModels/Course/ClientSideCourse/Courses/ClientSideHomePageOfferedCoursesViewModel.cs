using Kidemy.Application.ViewModels.Discount;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public class ClientSideHomePageOfferedCoursesViewModel : DiscountableViewModel<int>
    {
        public string Slug { get; set; }
        public string ImageFileName { get; set; }
        public CourseLevel Level { get; set; }
        public CourseType Type { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public double Score { get; set; }
        public int CommentsCount { get; set; }
        public int StudentsCount { get; set; }
        public List<TimeSpan> VideosLengths { get; set; }
        public int VideosCount { get; set; }
        public int MasterId { get; set; }
        public string MasterFullName { get; set; }
        public string MasterImageFileName { get; set; }
        public decimal? Price { get; set; }

        public override decimal GetMainPrice()
        {
            return Price ?? 0;
        }
    }
}
