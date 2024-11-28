using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories
{
    public class AdminSideCreateCategoryViewModel : LocalizableViewModel<LocalizedCreateCategoryViewModel>
    {
        [Display(Name = "ParentCategory")]
        public int? ParentCategoryId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }
        [Display(Name = "CategoryLogo")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "ParentCategories")]
        public List<AdminSideCategoryAsOptionViewModel>? Categories { get; set; }

        [Display(Name = "IsPopular")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public bool IsPopular { get; set; }
    }

    public class LocalizedCreateCategoryViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
    }
}
