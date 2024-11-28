using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.SocialMedia;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISocialMediaService _socialMediaService;
        private readonly ISiteSettingService _siteSettingService;

        public FooterViewComponent(ISocialMediaService socialMediaService, ISiteSettingService siteSettingService)
        {
            _socialMediaService = socialMediaService;
            _siteSettingService = siteSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var socialMedia = await _socialMediaService.FilterSocialMediaAsync(new FilterSocialMediaViewModel());
            ViewData["SocialMedia"] = socialMedia.Value;

            var siteSetting = await _siteSettingService.GetSiteSettingAsync();
            ViewData["SiteSetting"] = siteSetting.Value;

            return View("Footer");
        }
    }
}
