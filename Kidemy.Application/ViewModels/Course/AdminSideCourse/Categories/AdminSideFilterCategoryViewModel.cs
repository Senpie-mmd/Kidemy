using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories
{
    public class AdminSideFilterCategoryViewModel : BasePaging<AdminSideCategoryViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
    }
}
