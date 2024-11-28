using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Slider;
using Kidemy.Domain.Enums.DynamicText;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.DynamicText
{
    public class AdminSideUpdateDynamicTextViewModel : LocalizableViewModel<LocalizedAdminSideUpdateDynamicTextViewModel>
    {
        public int Id { get; set; }

        [Display(Name = "Text")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Text { get; set; }

        [Display(Name = "DynamicTextPosition")]
        public DynamicTextPosition Position { get; set; }

    }
    public class LocalizedAdminSideUpdateDynamicTextViewModel : LocalizedViewModel
    {
        [Display(Name = "Text")]
        [UIHint("textarea")]
        public string? Text { get; set; }

    }
}
