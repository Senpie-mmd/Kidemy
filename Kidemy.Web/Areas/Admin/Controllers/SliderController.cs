using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Slider;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageSliders")]
    public class SliderController : BaseAdminController
    {
        #region Fields

        private readonly ISliderService _sliderService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public SliderController(ISliderService sliderService, IStringLocalizer<SharedResource> localizer)
        {
            _sliderService = sliderService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [Permission("SlidersList")]
        public async Task<IActionResult> List(FilterSliderViewModel model)
        {
            model.TakeEntity = 20;
            var result = await _sliderService.FilterAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return View(result.Value);
        }

        #endregion

        #region Create

        [Permission("CreateSlider")]
        public IActionResult Create()
        {
            return View(new AdminSideCreateSliderViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("CreateSlider")]
        public async Task<IActionResult> Create(AdminSideCreateSliderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _sliderService.CreateSliderAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction("List", "Slider", new { area = "Admin" });
        }

        #endregion

        #region Update

        [Permission("UpdateSlider")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _sliderService.GetSliderForEditById(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List));
            }

            return View(result.Value);
        }

        [Permission("UpdateSlider")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpdateSliderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _sliderService.UpdateSliderAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Delete

        [Permission("DeleteSlider")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await _sliderService.DeleteSliderAsync(id);


            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #endregion
    }
}
