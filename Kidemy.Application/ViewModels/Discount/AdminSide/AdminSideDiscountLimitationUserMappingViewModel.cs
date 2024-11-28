using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideDiscountLimitationUserMappingViewModel
    {
        public int DiscountLimitationId { get; set; }

        [Display(Name = "User")]
        public int UserId { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }
    }
}
