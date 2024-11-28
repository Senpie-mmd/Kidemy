using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Discount;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideFilterDiscountViewModel : BasePaging<AdminSideDiscountViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Status")]
        public bool? IsActive { get; set; }

        [Display(Name = "DiscountCode")]
        public string? Code { get; set; }

        [Display(Name = "DiscountType")]
        public DiscountType? Type { get; set; }
    }
}
