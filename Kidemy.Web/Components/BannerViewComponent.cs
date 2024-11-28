using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Enums.Banner;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class BannerViewComponent : ViewComponent
    {

        #region Fields

        private readonly IBannerService _bannerService;
        private readonly IDynamicTextService _dynamicTextService;

        #endregion

        #region Ctor

        public BannerViewComponent(IBannerService bannerService, IDynamicTextService dynamicTextService)
        {
            _bannerService = bannerService;
            _dynamicTextService = dynamicTextService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(BannerPlace bannerPlace)
        {

            var banner = await _bannerService.GetBannerByPlaceForClientSide(bannerPlace);

            if (banner.IsFailure)
            {
                return View("Banner");
            }

            ViewData["BannerText"] =(await _dynamicTextService.GetDynamicTextByPosition(Domain.Enums.DynamicText.DynamicTextPosition.FirstPageBannerText)).Value.Text;
           
            return View("Banner", banner.Value);
        }

    }
}
