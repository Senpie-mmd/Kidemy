
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Link;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageLinks")]
    public class LinkController : BaseAdminController
    {
        #region Fields

        private readonly ILinkService _linkService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public LinkController(ILinkService linkService, IStringLocalizer<SharedResource> localizer)
        {
            _linkService = linkService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [Permission("LinksList")]
        public async Task<IActionResult> List(FilterLinkViewModel filter)
        {
            var result = await _linkService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View(result.Value);

        }
        #endregion

        #region Create

        [Permission("CreateLink")]
        public IActionResult Create()
        {
            return View(new AdminSideUpsertLinkViewModel());

        }

        [Permission("CreateLink")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideUpsertLinkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _linkService.CreateAsync(model);

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

        #region Update

        [Permission("UpdateLink")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _linkService.GetLinkForEditByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List), new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("UpdateLink")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpsertLinkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _linkService.UpdateAsync(model);

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

        [Permission("DeleteLink")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var result = await _linkService.DeleteAsync(id);

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
