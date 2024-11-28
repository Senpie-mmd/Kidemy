using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.User.AdminSide
{
    public enum UserStatus
    {
        [Display(Name = "Active")]
        Active,

        [Display(Name = "Inactive")]
        Inactive,

        [Display(Name = "IsBan")]
        IsBan,
    }
}
