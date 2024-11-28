using Barnamenevisan.Localizing.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideDiscountLimitationCategoryMappingViewModel
    {
        public int DiscountLimitationId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        [Translate(EntityName = "Category", Key = "Title", PropertyNameOfEntityIdInThisClass = nameof(CategoryId))]
        public string CategoryTitle { get; set; }
    }
}
