using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Discount
{
    public enum DiscountType
    {
        [Display(Name = "AssignedToOrderTotal")]
        AssignedToOrderTotal = 1,

        [Display(Name = "AssignedToCourses")]
        AssignedToCourses = 2
    }
}
