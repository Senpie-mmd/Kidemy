using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Discount;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail
{
    public class ClientSideMastersOtherCoursesViewModel : DiscountableViewModel<int>
    {
        public string slug { get; set; }
        public string ImageFileName { get; set; }
        public CourseType Type { get; set; }
        public int MasterId { get; set; }
        public string MasterAvatarFileName { get; set; }
        public double Score { get; set; }
        public int Students { get; set; }
        public string Title { get; set; }
        public string CategoryTitle { get; set; }
        public int CategoryId { get; set; }
        public decimal? Price { get; set; }

        public override decimal GetMainPrice()
        {
            return Price ?? 0;
        }
    }
}
