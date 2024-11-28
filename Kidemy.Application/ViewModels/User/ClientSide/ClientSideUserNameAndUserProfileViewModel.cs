using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.User.ClientSide
{
    public class ClientSideUserNameAndUserProfileViewModel : BaseEntityViewModel<int>
    {
        public string UserName { get; set; }
        public string? Mobile { get; set; }
        public string UserAvatar{ get; set; }
    }
}
