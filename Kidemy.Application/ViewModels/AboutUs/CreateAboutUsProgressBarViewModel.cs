using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.AboutUs
{
    public class CreateAboutUsProgressBarViewModel : LocalizableViewModel<LocalizedCreateAboutUsProgressBarViewModel>
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "Percent")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Range(0, 100, ErrorMessage = ErrorMessages.InvalidRangeError)]
        public int Percent { get; set; }
    }

    public class LocalizedCreateAboutUsProgressBarViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
    }
}
