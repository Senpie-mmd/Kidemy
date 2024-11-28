using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Banner;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Banner
{
    public class FilterBannerViewModel : BasePaging<AdminSideBannerViewModel>
    {
        [Display(Name = "Place")]
        public BannerPlace? Place { get; set; }

        [Display(Name = "IsPublished")]
        public bool? IsPublished { get; set; }
    }
}
