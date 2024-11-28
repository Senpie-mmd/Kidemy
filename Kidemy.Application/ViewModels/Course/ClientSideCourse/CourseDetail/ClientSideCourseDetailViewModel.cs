using Kidemy.Application.ViewModels.Discount;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail
{
    public class ClientSideCourseDetailViewModel : DiscountableViewModel<int>
    {
        public string ImageFileName { get; set; }
        public string DemoVideoFileName { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public CourseLevel Level { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int VideoCount { get; set; }
        public TimeSpan CourseVideosTotalTime { get; set; }
        public bool HasCertificate { get; set; }
        public decimal? Price { get; set; }
        public string Slug { get; set; }
        public CourseType Type { get; set; }
        public List<ClientSideCourseTagsViewModel> Tags { get; set; }
        public double AvrageScore { get; set; }
        public int? MasterId { get; set; }

        public override decimal GetMainPrice()
        {
            return Price ?? 0;
        }
    }
}
