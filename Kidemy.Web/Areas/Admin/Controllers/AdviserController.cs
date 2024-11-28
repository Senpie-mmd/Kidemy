using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Consultation.Adviser;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
     [Permission("ManageAdviser")]
    public class AdviserController : BaseAdminController
    {
        #region Fields
        private readonly IAdviserSerivce _adviserSerivce;
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion

        #region Constructor

        public AdviserController(IAdviserSerivce adviserSerivce, IStringLocalizer<SharedResource> localizer)
        {
            _adviserSerivce = adviserSerivce;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region Filter
        [Permission("AdviserList")]
        [HttpGet]
        public async Task<IActionResult> List(AdminSideFilterAdvisersViewModel model)
        {
            var lists = await _adviserSerivce.FilterAdvisers(model);
            return View(lists.Value);
        }

        #endregion

        #region Create
         [Permission("AddAdviser")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new AdminSideUpsertAdviserViewModel());
        }

         [Permission("AddAdviser")]
        [HttpPost]
        public async Task<IActionResult> Create(AdminSideUpsertAdviserViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _adviserSerivce.CreateAdviserAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        #endregion

        #region Edit
          [Permission("EditAdviser")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var adviser = await _adviserSerivce.GetForEditAdviserAsync(id);
            if (adviser.IsSuccess)
                return View(adviser.Value);


            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

         [Permission("EditAdviser")]
        [HttpPost]
        public async Task<IActionResult> Edit(AdminSideUpsertAdviserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _adviserSerivce.EditAdviserAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        #endregion

        #region Delete

         [Permission("DeleteAdviser")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var result = await _adviserSerivce.DeleteAdviserAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        #endregion

        #endregion
    }
}
