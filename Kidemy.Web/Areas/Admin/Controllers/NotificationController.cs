using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Notification;
using Kidemy.Application.ViewModels.User.AdminSide;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    public class NotificationController : BaseAdminController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IStringLocalizer _localizer;
        private readonly INotificationService _notificationService;

        #endregion

        #region Ctor
        public NotificationController(IUserService userService,
            IStringLocalizer localizer,
            INotificationService notificationService)
        {
            _localizer = localizer;
            _userService = userService;
            _notificationService = notificationService;
        }
        #endregion

        [Permission("SendNotification")]
        public IActionResult SendNotification()
        {
            return View();
        }

        [Permission("SendNotification")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SendNotification(SendNotificationViewModel notification)
        {
            var result = await _notificationService.SendNotification(notification);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(notification);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(NotificationList));
        }

        [Permission("FilterUsersForSendNotification")]
        public async Task<IActionResult> FilterUsersForSendNotification(FilterUsersViewModel model)
        {
            model.TakeEntity = 20;
            var result = await _userService.FilterUsersAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return PartialView("_SendNotificationUserFilter", result.Value);
        }

        #region NotificationList

        [Permission("NotificationList")]
        public async Task<IActionResult> NotificationList(FilterNotificationViewModel filter)
        {

            var result = await _notificationService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(filter);
            }

            return View(result.Value);

        }
        #endregion

        #region UpdateMasterNotification

        [Permission("UpdateNotification")]
        public async Task<IActionResult> UpdateNotification(int id)
        {
            var result = await _notificationService.GetNotificationForEditById(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(NotificationList), new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("UpdateNotification")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNotification(AdminSideUpdateNotificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _notificationService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(NotificationList), new { area = "Admin" });

        }
        #endregion

        #region DeleteNotification

        [Permission("DeleteNotification")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await _notificationService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(NotificationList));
        }

        #endregion
    }
}
