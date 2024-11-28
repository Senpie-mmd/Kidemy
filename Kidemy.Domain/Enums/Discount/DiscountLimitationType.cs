using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Discount
{
    public enum DiscountLimitationType
    {
        [Display(Name = "DiscountLimitationType.User")]
        User,

        [Display(Name = "DiscountLimitationType.Course")]
        Course,

        [Display(Name = "DiscountLimitationType.Category")]
        Category,

        [Display(Name = "DiscountLimitationType.UsageCount")]
        UsageCount
    }
}
