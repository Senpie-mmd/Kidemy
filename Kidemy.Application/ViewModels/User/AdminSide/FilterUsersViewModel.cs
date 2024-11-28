using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.User.AdminSide
{
    public class FilterUsersViewModel : BasePaging<AdminSideUserViewModel>
    {
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }

        [Display(Name = "LastName")]
        public string? LastName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }

        [Display(Name = "Status")]
        public UserStatus? Status { get; set; }

        [Display(Name = "RoleId")]
        public int? RoleId { get; set; }

        [Display(Name = "IsMobileActive")]
        public bool? IsMobileActive { get; set; }

        public bool? IsMobileExist { get; set; }

        public bool? IsEmailExist { get; set; }

        public int? UserId { get; set; }
    }
}
