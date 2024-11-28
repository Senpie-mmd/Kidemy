using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideDiscountLimitationUsageCountMappingViewModel
    {
        public int DiscountLimitationId { get; set; }

        [Display(Name = "UsageCount")]
        public int Count { get; set; }
    }
}
