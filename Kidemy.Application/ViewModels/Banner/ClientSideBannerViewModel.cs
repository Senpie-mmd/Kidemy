using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Banner;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;

namespace Kidemy.Application.ViewModels.Banner
{
    public class ClientSideBannerViewModel : BaseEntityViewModel<int>
    {
        public ClientSideBannerViewModel()
        {
            ImageName = SiteTools.DefaultImageName;
        }

        public string ImageName { get; set; }
        public BannerPlace Place { get; set; }
        public string? URL { get; set; }
        public bool IsPublished { get; set; }
    }
}
