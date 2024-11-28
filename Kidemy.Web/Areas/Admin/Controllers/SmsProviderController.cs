using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.SmsProvider;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    public class SmsProviderController : BaseAdminController
    {
        #region Fields

        private readonly ISmsProviderService _smsProviderService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public SmsProviderController(ISmsProviderService smsProviderService, IStringLocalizer<SharedResource> localizer)
        {
            _smsProviderService = smsProviderService;
            _localizer = localizer;
        }

        #endregion

        #region List

        [Permission("SmsProviderList")]
        public async Task<IActionResult> List(FilterSmsProviderViewModel model)
        {
            model.TakeEntity = 20;
            var result = await _smsProviderService.FilterAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return View(result.Value);
        }

        #endregion

        #region Update
        [Permission("UpdateSmsProvider")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _smsProviderService.GetSmsProviderForEditAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(new AdminSideUpsertSmsProviderViewModel());
            }

            return View(result.Value);

        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("UpdateSmsProvider")]
        public async Task<IActionResult> Update(AdminSideUpsertSmsProviderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _smsProviderService.UpdateSmsProviderAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }


            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}
