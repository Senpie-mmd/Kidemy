using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories
{
    public class AdminSideUpdateCategoryViewModel : LocalizableViewModel<LocalizedAdminSideUpdateCategoryViewModel>
    {
        public AdminSideUpdateCategoryViewModel()
        {
            ImageName = SiteTools.DefaultImageName;
        }

        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        public string? ImageName { get; set; }

        [Display(Name = "CategoryLogo")]
        public IFormFile? CategoryImage { get; set; }

        [Display(Name = "ParentCategory")]
        public int? ParentCategoryId { get; set; }

        [Display(Name = "ParentCategories")]
        public List<AdminSideCategoryAsOptionViewModel>? Categories { get; set; }

        [Display(Name = "IsPopular")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public bool IsPopular { get; set; }
    }

    public class LocalizedAdminSideUpdateCategoryViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
    }
}
