using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Slider;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Slider
{
    public class AdminSideCreateSliderViewModel : LocalizableViewModel<LocalizedCreateSliderViewModel>
    {
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        [MaxLength(900, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Description { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public IFormFile Image { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.OutOfRangeError)]
        public int Priority { get; set; }

        [Display(Name = "SliderPlace")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public SliderPlace SliderPlace { get; set; }

        [Display(Name = "URL")]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? URL { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }

    public class LocalizedCreateSliderViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        [UIHint("Textarea")]
        [MaxLength(900, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Description { get; set; }

    }
}
