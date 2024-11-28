using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;


namespace Kidemy.Application.ViewModels.Role
{
    public class AdminSideRoleViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Translate(EntityName = nameof(Role))]
        public string Title { get; set; }

        [Display(Name = "UniqueName")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string UniqueName { get; set; }
    }
}
