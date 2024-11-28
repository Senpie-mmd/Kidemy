using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class SiteLogoViewComponent : ViewComponent
    {
        private readonly ISiteSettingService _siteSettingService;

        public SiteLogoViewComponent(ISiteSettingService siteSettingService)
        {
            _siteSettingService = siteSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var siteSetting = await _siteSettingService.GetSiteSettingAsync();
            ViewData["SiteSetting"] = siteSetting.Value;

            return View("SiteLogo");
        }
    }
}
