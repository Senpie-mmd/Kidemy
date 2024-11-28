using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Application.ViewModels.MasterNotification;
using Kidemy.Application.ViewModels.Notification;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Application.ViewModels.User.AdminSide;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageMastersNotifications")]
    public class MasterNotificationController : BaseAdminController
    {
        #region Fields
        private readonly IMasterService _masterService;
        private readonly IStringLocalizer _localizer;
        private readonly IMasterNotificationService _masterNotificationService;
        #endregion

        #region Ctor
        public MasterNotificationController(IMasterService masterService,
            IStringLocalizer localizer,
            IMasterNotificationService masterNotificationService)
        {
            _localizer = localizer;
            _masterService = masterService;
            _masterNotificationService = masterNotificationService;
        }
        #endregion

        [Permission("SendMastersNotifications")]
        public IActionResult SendMasterNotification()
        {
            return View(new SendMasterNotificationViewModel());
        }

        [Permission("SendMastersNotifications")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMasterNotification(SendMasterNotificationViewModel notification)
        {
            var result = await _masterNotificationService.SendMasterNotification(notification);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(notification);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(MasterNotificationList));
        }


        public async Task<IActionResult> FilterMastersForSendNotification(FilterForAdminSideMasterViewModel model)
        {
            model.TakeEntity = 20;
            var result = await _masterService.FilterForAdminSideAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return PartialView("_SendNotificationMasterFilter", result.Value);
        }

        #region MasterNotificationList

        [Permission("MastersNotificationsList")]
        public async Task<IActionResult> MasterNotificationList(FilterMasterNotificationViewModel filter)
        {

            var result = await _masterNotificationService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(filter);
            }

            return View(result.Value);

        }
        #endregion

        #region UpdateMasterNotification

        [Permission("UpdateMasterNotification")]
        public async Task<IActionResult> UpdateMasterNotification(int id)
        {
            var result = await _masterNotificationService.GetMasterNotificationForEditById(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(MasterNotificationList), new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("UpdateMasterNotification")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMasterNotification(AdminSideUpdateMasterNotificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _masterNotificationService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(MasterNotificationList), new { area = "Admin" });

        }
        #endregion

        #region DeleteMasterNotification

        [Permission("DeleteMasterNotification")]
        public async Task<IActionResult> DeleteMasterNotification(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await _masterNotificationService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(MasterNotificationList));
        }

        #endregion

    }
}
