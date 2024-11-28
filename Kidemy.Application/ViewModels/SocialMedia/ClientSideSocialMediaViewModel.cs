using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.SocialMedia
{
    public class ClientSideSocialMediaViewModel : BaseEntityViewModel<int>
    {
        public int Priority { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string ImageName { get; set; }
    }
}
