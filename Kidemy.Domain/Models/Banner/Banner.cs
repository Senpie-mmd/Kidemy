using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Banner;

namespace Kidemy.Domain.Models.Banner
{
    public class Banner : BaseEntity<int>
    {
        public string ImageName { get; set; }
        public BannerPlace Place { get; set; }
        public string? URL { get; set; }
        public bool IsPublished { get; set; }
    }
}
