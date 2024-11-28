using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.AboutUs
{
    public class CreateAboutUsFeatureViewModel : LocalizableViewModel<LocalizedCreateAboutUsFeatureViewModel>
    {
        [Display(Name = "Body")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string body { get; set; }
    }

    public class LocalizedCreateAboutUsFeatureViewModel : LocalizedViewModel
    {
        [Display(Name = "Body")]
        public string? body { get; set; }
    }
}
