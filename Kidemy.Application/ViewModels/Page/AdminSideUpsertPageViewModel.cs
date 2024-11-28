using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Page;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Page
{
    public class AdminSideUpsertPageViewModel : LocalizableViewModel<LocalizedAdminSideUpsertPageViewModel>
    {

        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "Body")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Body { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Slug { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }

        [Display(Name = "LinkPlace")]
        public LinkPlace LinkPlace { get; set; }
    }

    public class LocalizedAdminSideUpsertPageViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Body")]
        [UIHint("ckeditor")]
        public string? Body { get; set; }
    }
}
