using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Site;
using Kidemy.Application.ViewModels.UserPanel;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.UserPanel.Components
{
    public class UserPanelSideBarViewComponent : ViewComponent
    {
        #region Fields

        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public UserPanelSideBarViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = User.GetUserId();
            bool userIsMaster = false;
            var userIsMasterResult = await _userService.UserIsMasterAsync(userId);

            if (userIsMasterResult.IsSuccess)
            {
                userIsMaster = userIsMasterResult.Value;
            }

            ViewData["UserIsMaster"] = userIsMaster;
            ViewData["UserId"] = userId;

            return View("UserPanelSideBar");
        }
    }
}
