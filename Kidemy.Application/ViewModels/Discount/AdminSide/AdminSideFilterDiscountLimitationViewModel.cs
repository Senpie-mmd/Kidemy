using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Discount;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideFilterDiscountLimitationViewModel : BasePaging<AdminSideDiscountLimitationViewModel>
    {
        public int? DiscountId { get; set; }

        [Display(Name = "DiscountLimitationType")]
        public DiscountLimitationType? Type { get; set; }
    }
}
