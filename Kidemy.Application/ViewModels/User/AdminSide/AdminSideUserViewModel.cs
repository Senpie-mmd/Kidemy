using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.User.AdminSide
{
    public class AdminSideUserViewModel : BaseEntityViewModel<int>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public List<int> Roles { get; set; }

        public UserStatus Status { get; set; }
    }
}
