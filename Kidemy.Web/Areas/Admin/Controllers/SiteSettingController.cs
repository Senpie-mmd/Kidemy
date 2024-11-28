using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.SiteSetting;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    public class SiteSettingController : BaseAdminController
    {
        #region Fields

        private readonly ISiteSettingService _siteSettingService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public SiteSettingController(ISiteSettingService siteSettingService, IStringLocalizer<SharedResource> localizer)
        {
            _siteSettingService = siteSettingService;
            _localizer = localizer;
        }

        #endregion

        #region Update
        [Permission("UpdateSiteSetting")]
        public async Task<IActionResult> Update()
        {

            var result = await _siteSettingService.GetSiteSettingForEditAsync();

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(new AdminSideUpsertSiteSettingViewModel());
            }

            return View(result.Value);

        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("UpdateSiteSetting")]
        public async Task<IActionResult> Update(AdminSideUpsertSiteSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _siteSettingService.UpdateSiteSettingAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }


            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(Update), new {area="Admin"});
        }
        #endregion
    }
}
