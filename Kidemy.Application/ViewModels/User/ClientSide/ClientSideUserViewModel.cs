using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.User.ClientSide
{
    public class ClientSideUserViewModel : BaseEntityViewModel<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public bool IsMobileActive { get; set; }

        public string Email { get; set; }

        public string AvatarName { get; set; }

        public string GetUserFullName()
        {
            if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
                return Mobile;
            else
                return $"{FirstName} {LastName}";
        }
    }
}
