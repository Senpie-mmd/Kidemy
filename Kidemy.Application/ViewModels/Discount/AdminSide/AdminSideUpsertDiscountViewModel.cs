using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Discount;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideUpsertDiscountViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }

        [Display(Name = "DiscountCode")]
        [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Code { get; set; }

        [Display(Name = "DiscountValue")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public decimal? Value { get; set; }

        [Display(Name = "IsPercentage")]
        public bool IsPercentage { get; set; }

        [Display(Name = "StartDateTimeOnUtc")]
        public string? StartDateTime { get; set; }

        [Display(Name = "EndDateTimeOnUtc")]
        public string? EndDateTime { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "AutoUse")]
        public bool AutoUse { get; set; }

        [Display(Name = "DiscountType")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public DiscountType Type { get; set; }
    }
}
