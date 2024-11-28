using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.User;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.User.AdminSide
{
    public class AdminSideUpsertUserViewModel : BaseEntityViewModel<int>
    {
        public AdminSideUpsertUserViewModel()
        {
            AvatarName = SiteTools.DefaultImageName;
        }

        #region Properties
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailFormatError)]
        [MaxLength(350, ErrorMessage = ErrorMessages.MaxLengthError)]
        [DataType(DataType.EmailAddress, ErrorMessage = ErrorMessages.EmailFormatError)]
        public string? Email { get; set; }

        [Display(Name = "Mobile")]
        [MaxLength(15, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [RegularExpression("^[0-9]9\\d{9}$", ErrorMessage = ErrorMessages.MobileFormatError)]
        public string? Mobile { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? LastName { get; set; }

        [Display(Name = "Password")]
        [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Password { get; set; }

        [Display(Name = "IsEmailActive")]
        public bool IsEmailActive { get; set; }

        [Display(Name = "IsMobileActive")]
        public bool IsMobileActive { get; set; }

        [Display(Name = "AvatarName")]
        [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? AvatarName { get; set; }

        [Display(Name = "IsBan")]
        public bool IsBan { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string? BirthDate { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        public IFormFile? AvatarFile { get; set; }

        public List<int>? RoleIds { get; set; }

        #endregion

        #region Methods

        public string GetUserFullName()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
                return $"{FirstName} {LastName}";
            else
                return Mobile;
        }

        #endregion

    }
}
