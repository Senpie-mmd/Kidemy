using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class SubscribeToNewsletterViewComponent : ViewComponent
    {
        #region Fields

        private readonly ISiteSettingService _siteSettingService;

        #endregion

        #region Ctor

        public SubscribeToNewsletterViewComponent(ISiteSettingService siteSettingService)
        {
            _siteSettingService = siteSettingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _siteSettingService.GetSiteSettingAsync();
            return View("SubscribeToNewsletter", result.Value);
        }
    }
}
