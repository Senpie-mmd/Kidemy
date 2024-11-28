using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.User;
using Kidemy.Domain.Statics;

namespace Kidemy.Domain.Models.User
{
    public class User : AuditBaseEntity<int>
    {
        public User()
        {
            Roles = new List<UserRoleMapping>();
            AvatarName = SiteTools.DefaultImageName;
        }

        #region Properties


        public string? Email { get; set; }


        public string Mobile { get; set; }


        public Gender Gender { get; set; }


        public string? FirstName { get; set; }


        public string? LastName { get; set; }


        public string? Password { get; set; }

        public bool IsEmailActive { get; set; }

        public bool IsMobileActive { get; set; }


        public string? AvatarName { get; set; }

        public bool IsBan { get; set; }
        public bool UnableToWithdraw { get; set; }

        public DateTime? BirthDateOnUtc { get; set; }
        #endregion

        #region Relations

        public ICollection<UserRoleMapping> Roles { get; set; }

        #endregion
    }
}
