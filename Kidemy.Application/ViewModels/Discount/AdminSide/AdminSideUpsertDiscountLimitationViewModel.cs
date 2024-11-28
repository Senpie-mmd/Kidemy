using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Discount;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideUpsertDiscountLimitationViewModel : BaseEntityViewModel<int>
    {
        public int DiscountId { get; set; }

        [Display(Name = "DiscountLimitationType")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public DiscountLimitationType? Type { get; set; }

        [ValidateNever]
        public List<AdminSideDiscountLimitationUserMappingViewModel>? Users { get; set; }

        [ValidateNever]
        public List<AdminSideDiscountLimitationCourseMappingViewModel>? Courses { get; set; }

        [ValidateNever]
        public List<AdminSideDiscountLimitationCategoryMappingViewModel>? Categories { get; set; }

        [ValidateNever]
        public AdminSideDiscountLimitationUsageCountMappingViewModel? UsageCount { get; set; }

    }
}
