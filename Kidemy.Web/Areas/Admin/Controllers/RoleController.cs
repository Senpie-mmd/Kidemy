using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Role;
using Kidemy.Domain.Shared;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Permission = Kidemy.Web.Attributes.Permission;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageRoles")]
    public class RoleController : BaseAdminController
    {
        #region Fields

        private readonly IRoleService _roleService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public RoleController(IRoleService roleService, IStringLocalizer<SharedResource> localizer)
        {
            _roleService = roleService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [Permission("RolesList")]
        public async Task<IActionResult> List(FilterRoleViewModel model)
        {
            var result = await _roleService.FilterRolesAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(model);
        }

        #endregion

        #region Create

        [Permission("CreateRole")]
        public async Task<IActionResult> Create()
        {
            return View(new AdminSideUpsertRoleViewModel());
        }

        [Permission("CreateRole")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideUpsertRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _roleService.CreateRoleAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction("List");
        }

        #endregion

        #region Update

        [Permission("UpdateRole")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _roleService.GetRoleById(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List));
            }

            return View(result.Value);
        }

        [Permission("UpdateRole")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpsertRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _roleService.UpdateRoleAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction("List");
        }

        #endregion

        #region Delete

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("DeleteRole")]
        public async Task<ResponseResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return new ResponseResult(false);
            }

            var result = await _roleService.DeleteRoleAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return new ResponseResult(false);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return new ResponseResult(true);
        }

        #endregion

        #endregion
    }
}
