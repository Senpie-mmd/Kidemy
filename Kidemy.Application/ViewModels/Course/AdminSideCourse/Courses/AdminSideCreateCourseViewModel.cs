using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses
{
    public class AdminSideCreateCourseViewModel : LocalizableViewModel<LocalizedAdminSideCreateCourseViewModel>
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "ShortDescription")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string ShortDescription { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Description { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Slug { get; set; }

        [Display(Name = "CourseTags")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string CourseTags { get; set; }

        [Display(Name = "CourseLevel")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public CourseLevel Level { get; set; }

        [Display(Name = "CourseStatus")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public CourseStatus Status { get; set; }

        [Display(Name = "IsOffer")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public bool IsOffer { get; set; }

        [Display(Name = "MasterId")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int MasterId { get; set; }

        [Display(Name = "Price")]
        [Range(1, 999999999, ErrorMessage = ErrorMessages.InvalidRangeError)]
        public decimal? Price { get; set; }

        [Display(Name = "HasCertificate")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public bool HasCertificate { get; set; }

        [Display(Name = "AllowComenting")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public bool AllowComenting { get; set; }

        [Display(Name = "CourseType")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public CourseType Type { get; set; }

        [Display(Name = "IsPublished")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public bool IsPublished { get; set; }

        [Display(Name = "CourseImageFileName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public IFormFile CourseImageFileName { get; set; }

        [Display(Name = "CourseDemoVideoFileName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string DemoVideoFileName { get; set; }

        public string? MasterFullName { get; set; }

        [Display(Name = "MasterCommissionPercentage")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int? MasterCommissionPercentage { get; set; }

        [Display(Name = "FileSuffix")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string FileSuffix { get; set; }

        public List<AdminSideCategoryAsOptionViewModel>? Categories { get; set; }

        [Display(Name = "CourseCategory")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public List<int> CourseCategoryIds { get; set; }
    }
    public class LocalizedAdminSideCreateCourseViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "ShortDescription")]
        [UIHint("textarea")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Description")]
        [UIHint("ckeditor")]
        public string? Description { get; set; }
    }
}
