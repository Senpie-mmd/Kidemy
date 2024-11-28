using Barnamenevisan.Localizing.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideDiscountLimitationCourseMappingViewModel
    {
        public int DiscountLimitationId { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Display(Name = "Course")]
        [Translate(EntityName = "Course", Key = "Title", PropertyNameOfEntityIdInThisClass = nameof(CourseId))]
        public string CourseTitle { get; set; }
    }
}
