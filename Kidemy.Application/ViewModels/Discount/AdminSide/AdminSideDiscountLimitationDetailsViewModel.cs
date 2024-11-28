using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Discount;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideDiscountLimitationDetailsViewModel : BaseEntityViewModel<int>
    {
        public int DiscountId { get; set; }

        public DiscountLimitationType? Type { get; set; }

        public List<AdminSideDiscountLimitationUserMappingViewModel>? Users { get; set; }

        public List<AdminSideDiscountLimitationCourseMappingViewModel>? Courses { get; set; }

        public List<AdminSideDiscountLimitationCategoryMappingViewModel>? Categories { get; set; }

        public AdminSideDiscountLimitationUsageCountMappingViewModel? UsageCount { get; set; }
    }
}
