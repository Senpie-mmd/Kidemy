using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class HomeController : BaseUserPanelController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IMasterService _masterService;
        private readonly IDynamicTextService _dynamicTextService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public HomeController(IUserService userService, IStringLocalizer<SharedResource> localizer, IDynamicTextService dynamicTextService, IMasterService masterService)
        {
            _userService = userService;
            _localizer = localizer;
            _dynamicTextService = dynamicTextService;
            _masterService = masterService;
        }

        #endregion

        #region Actions

        #region Index

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.GetUserId();
            ViewData["UserId"] = userId;
            ViewData["IsMaster"] = (await _userService.UserIsMasterAsync(userId)).Value;

            return View();
        }

        #endregion

        #region EditProfile

        [HttpGet("userpanel/edit-profile")]
        public async Task<IActionResult> EditProfile()
        {
            var userId = HttpContext.User.GetUserId();
            var result = await _userService.GetUserProfileForEditByIdAsync(userId);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return Redirect("/");
            }

            var model = result.Value;
            await FillUserAvatarNameForView();

            return View(model);
        }

        [HttpPost("userpanel/edit-profile"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ClientSideUpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await FillUserAvatarNameForView();
                return View(model);
            }

            model.Id = HttpContext.User.GetUserId();
            Result result = await _userService.UpdateUserProfileAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            await FillUserAvatarNameForView();
            return View(model);
        }
        #endregion

        #region Utilities

        private async Task FillUserAvatarNameForView()
        {
            var userAvatarResult = await _userService.GetUserAvatarNameByIdAsync(User.GetUserId());
            var userAvatarName = SiteTools.DefaultImageName;
            if (userAvatarResult.IsSuccess)
            {
                userAvatarName = userAvatarResult.Value;
            }

            TempData["UserAvatarName"] = userAvatarName;
        }

        #endregion

        #endregion

    }
}
