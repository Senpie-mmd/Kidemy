using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kidemy.Web.Components
{
    public class NavProfileViewComponent : ViewComponent
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public NavProfileViewComponent(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            bool userIsAdmin = false;
            var userIsAdminResult = _roleService.UserHasPermissionAsync(User.GetUserId(), "AdminPanel");
            if (userIsAdminResult.Result.IsSuccess)
            {
                userIsAdmin = userIsAdminResult.Result.Value;
            }
            ViewData["UserIsAdmin"] = userIsAdmin;

            string? userName = User.Identity?.Name;
            var userNameResult = await _userService.GetUserFullName(User.GetUserId());
            if (userNameResult.IsSuccess)
            {
                userName = userNameResult.Value;
            }
            ViewData["UserName"] = userName;

            string? email = "";
            var emailResult = await _userService.GetUserEmailByIdAsync(User.GetUserId());
            if (emailResult.IsSuccess)
            {
                email = emailResult.Value;
            }
            ViewData["Email"] = email;

            string? avatrName = "";
            var avatrNameResult = await _userService.GetUserAvatarNameByIdAsync(User.GetUserId());
            if (avatrNameResult.IsSuccess)
            {
                avatrName = avatrNameResult.Value;
            }
            ViewData["AvatarName"] = avatrName;

            string? mobile = HttpContext.User.FindFirstValue(ClaimTypes.MobilePhone);
            ViewData["Mobile"] = mobile;

            return View("NavProfile");
        }
    }
}
