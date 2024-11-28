using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.User;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.User.ClientSide
{
    public class ClientSideUpdateUserViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailFormatError)]
        [MaxLength(350, ErrorMessage = ErrorMessages.MaxLengthError)]
        [DataType(DataType.EmailAddress, ErrorMessage = ErrorMessages.EmailFormatError)]
        public string? Email { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string? BirthDate { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public Gender Gender { get; set; }

        [Display(Name = "Avatar")]
        public IFormFile? Avatar { get; set; }
    }
}
