using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace Kidemy.Application.ViewModels.AboutUs
{
    public class AdminSideUpsertAboutUsViewModel : LocalizableViewModel<LocalizedAdminSideUpsertAboutUsViewModel>
    {
        [Display(Name = "PageTitle")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? PageTitle { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(4000, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Description { get; set; }

        [Display(Name = "OurGoal")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? OurGoal { get; set; }

        [Display(Name = "OurGoalTitle")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? OurGoalTitle { get; set; }

        [Display(Name = "OurGoalDescription")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(5000, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? OurGoalDescription { get; set; }

        [Display(Name = "OurGoalFeatures")]
        public string? OurGoalFeatures { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber1 { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber2 { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber3 { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber4 { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber5 { get; set; }

        public IFormFile? ImageFileNumber1 { get; set; }
        public IFormFile? ImageFileNumber2 { get; set; }
        public IFormFile? ImageFileNumber3 { get; set; }
        public IFormFile? ImageFileNumber4 { get; set; }
        public IFormFile? ImageFileNumber5 { get; set; }
    }

    public class LocalizedAdminSideUpsertAboutUsViewModel : LocalizedViewModel
    {
        [Display(Name = "PageTitle")]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? PageTitle { get; set; }

        [Display(Name = "Title")]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        [MaxLength(4000, ErrorMessage = ErrorMessages.MaxLengthError)]
        [UIHint("ckeditor")]
        public string? Description { get; set; }

        [Display(Name = "OurGoal")]
        [MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? OurGoal { get; set; }

        [Display(Name = "OurGoalTitle")]
        [MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? OurGoalTitle { get; set; }

        [UIHint("ckeditor")]
        [Display(Name = "OurGoalDescription")]
        [MaxLength(5000, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? OurGoalDescription { get; set; }

        [Display(Name = "OurGoalFeatures")]
        [UIHint("tag")]
        public string? OurGoalFeatures { get; set; }
    }
}
