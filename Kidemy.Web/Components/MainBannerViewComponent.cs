using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.DynamicText;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class MainBannerViewComponent : ViewComponent
    {
        #region Fields

        private readonly IDynamicTextService _dynamicTextService;

        #endregion

        #region Ctor

        public MainBannerViewComponent(IDynamicTextService dynamicTextService)
        {
            _dynamicTextService = dynamicTextService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _dynamicTextService.GetMainBannerText();

            if (result.IsFailure)
            {
                return View("MainBanner", new List<ClientSideDynamicTextViewModel>());
            }

            return View("MainBanner", result.Value);
        }
    }
}
