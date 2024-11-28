using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course
{
    public enum CoursePriceTypeForView
    {
        [Display(Name = "All")]
        All,
        [Display(Name = "Free")]
        Free,
        [Display(Name = "Mercenary")]
        Mercenary,
        [Display(Name = "ForVIPMembers")]
        VIP
    }
}
