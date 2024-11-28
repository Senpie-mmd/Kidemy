using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Course;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;

namespace Kidemy.Application.ViewModels.Order;

public class AdminSideOrderItemViewModel : BaseEntityViewModel<int>
{
    public int OrderId { get; set; }

    public int CourseId { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? DiscountedUnitPrice { get; set; }

    public CourseShortDetailsViewModel Course { get; set; }
}