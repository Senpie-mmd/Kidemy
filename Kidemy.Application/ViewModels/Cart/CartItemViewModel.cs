using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;

namespace Kidemy.Application.ViewModels.Cart
{
    public class CartItemViewModel : BaseEntityViewModel<int>
    {
        public int CartId { get; set; }

        public int CourseId { get; set; }

        public ClientSideCourseShortDetailsViewModel Course { get; set; }
    }
}
