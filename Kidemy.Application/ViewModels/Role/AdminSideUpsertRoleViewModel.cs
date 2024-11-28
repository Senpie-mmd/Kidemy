using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Role
{
    public class AdminSideUpsertRoleViewModel : LocalizableViewModel<LocalizedAdminSideUpsertRoleViewModel>
    {
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "UniqueName")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string UniqueName { get; set; }

        public List<int>? PermissionsId { get; set; }
    }

    public class LocalizedAdminSideUpsertRoleViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }
    }
}
