using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.Shared;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class ChangePasswordController : BaseUserPanelController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public ChangePasswordController(IUserService userService, IStringLocalizer<SharedResource> localizer)
        {
            _userService = userService;
            _localizer = localizer;
        }

        #endregion

        #region Methods

        [HttpGet("userpanel/change-password")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("userpanel/change-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _userService.UpdateUserPassword(User.GetUserId(), model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.PasswordChangedSuccessfully].ToString();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
