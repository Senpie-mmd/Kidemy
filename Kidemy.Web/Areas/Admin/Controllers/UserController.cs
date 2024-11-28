using Kidemy.Application.Convertors;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.User.AdminSide;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageUsers")]
    public class UserController : BaseAdminController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IVIPMembersService _vIPMembersService;
        private readonly IVIPPlanService _vIPPlanService;
        private readonly IRoleService _roleService;
        private readonly IStringLocalizer _localizer;

        #endregion

        #region Constructor

        public UserController(IUserService userService,
            IRoleService roleService,
            IStringLocalizer localizer,
            IVIPMembersService vIPMembersService,
            IVIPPlanService vIPPlanService)
        {
            _userService = userService;
            _roleService = roleService;
            _localizer = localizer;
            _vIPMembersService = vIPMembersService;
            _vIPPlanService = vIPPlanService;
        }

        #endregion

        #region Actions

        [Permission("UserList")]
        public async Task<IActionResult> List(FilterUsersViewModel model)
        {
            var result = await _userService.FilterUsersAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return View(result.Value);
        }

        [Permission("CreateUser")]
        public async Task<IActionResult> Create()
        {
            ViewData["AvailableRoles"] = (await _roleService.GetAllRolesAsync()).Value;
            return View(new AdminSideUpsertUserViewModel());
        }

        [Permission("CreateUser")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideUpsertUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["AvailableRoles"] = (await _roleService.GetAllRolesAsync()).Value;
                return View(model);
            }

            var result = await _userService.CreateUserAsync(model);

            if (result.IsFailure)
            {
                ViewData["AvailableRoles"] = (await _roleService.GetAllRolesAsync()).Value;
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.UserCreatedSuccessfully].ToString();

            return RedirectToAction(nameof(List));
        }

        [Permission("UpdateUser")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            var result = await _userService.GetUserForEditInAdminSideByIdAsync((int)id);
            ViewData["AvailableRoles"] = (await _roleService.GetAllRolesAsync()).Value;

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List));
            }

            return View(result.Value);
        }

        [Permission("UpdateUser")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpsertUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["AvailableRoles"] = (await _roleService.GetAllRolesAsync()).Value;
                return View(model);
            }
            var result = await _userService.UpdateUserByAdminAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                ViewData["AvailableRoles"] = (await _roleService.GetAllRolesAsync()).Value;
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.UserUpdatedSuccessfully].ToString();

            return RedirectToAction(nameof(List));
        }


        [Permission("DeleteUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            var result = await _userService.DeleteUserByAdminAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            TempData[SuccessMessage] = SuccessMessages.UserDeletedSuccessfully.ToString();

            return RedirectToAction(nameof(List));
        }

        [Permission("UserProfile")]
        public async Task<IActionResult> Profile(int id)
        {
            var result = await _userService.GetUserInfoAsync(id);
            var userIsVIPMember = await _userService.IsUserVIPMemberAsync(id);
            var vIPMemberInformation = await _vIPMembersService.GetVIPUserInformation(id);
            DateTime membershipEndDate = vIPMemberInformation.Value.MembershipEndDate;
            bool userIsMaster = false;
            var userIsMasterResult = await _userService.UserIsMasterAsync(id);

            if (userIsMasterResult.IsSuccess)
            {
                userIsMaster = userIsMasterResult.Value;
            }

            var vIPPlans = await _vIPPlanService.GetAllVIPPlansForAdmin();


            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List));
            }

            ViewData["UserIsVIPMember"] = userIsVIPMember.Value;
            ViewData["MembershipEndDate"] = membershipEndDate.ToUserDate();
            ViewData["VIPPlans"] = vIPPlans.Value;
            ViewData["UserIsMaster"] = userIsMaster;
            return View(result.Value);
        }

        public async Task<IActionResult> ExpirationVIPAccount(int id)
        {

            var result = await _vIPMembersService.ExpirationVIPAccountAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.UserAccountExpiredSuccessfully].ToString();

            return RedirectToAction(nameof(Profile), new { id = id });
        }



        #region Unable To Withdraw
        [Permission("UnableToWithdrawUser")]
        public async Task<IActionResult> UnableToWithdrawUser(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            var result = await _userService.UnableToWithdrawUser(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            TempData[SuccessMessage] = SuccessMessages.UserDeletedSuccessfully.ToString();

            return RedirectToAction(nameof(Profile), new { id = id });
        }
        #endregion

        

        #region able To Withdraw
         [Permission("AbleToWithdrawUser")]
        public async Task<IActionResult> AbleToWithdrawUser(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            var result = await _userService.AbleToWithdrawUser(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            TempData[SuccessMessage] = SuccessMessages.UserDeletedSuccessfully.ToString();

            return RedirectToAction(nameof(Profile), new { id = id });
        }
        #endregion

        #endregion

    }
}


