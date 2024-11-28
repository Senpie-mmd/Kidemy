using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.Discount.ClientSide;

namespace Kidemy.Application.ViewModels.Discount
{
    public abstract class DiscountableViewModel<TKey> : BaseEntityViewModel<TKey>
    {
        public decimal? DiscountedPrice { get; set; }

        public ClientSideDiscountViewModel? AppliedDiscount { get; set; }

        public abstract decimal GetMainPrice();
    }

    public abstract class CustomDiscountableViewModel<TKey> : BaseEntityViewModel<TKey>
    {
        public decimal? DiscountedPrice { get; set; }

        public ClientSideOfferedCoursesViewModel? AppliedDiscount { get; set; }

        public abstract decimal GetMainPrice();
    }
}
