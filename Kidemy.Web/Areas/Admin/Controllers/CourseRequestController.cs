using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.CourseRequest;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageCourseRequests")]
    public class CourseRequestController : BaseAdminController
    {
        #region Fields

        private readonly ICourseRequestService _courseRequestService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public CourseRequestController(ICourseRequestService courseRequestService, IStringLocalizer<SharedResource> localizer)
        {
            _courseRequestService = courseRequestService;
            _localizer = localizer;
        }

        #endregion

        #region List

        [Permission("CourseRequestsList")]
        public async Task<IActionResult> List(FilterForAdminSideCourseRequestViewModel filter)
        {
            var result = await _courseRequestService.FilterForAdminSideAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(filter);
            }

            return View(result.Value);

        }

        #endregion

        #region ConfrimCourseRequest

        [Permission("ConfirmCourseRequest")]
        public async Task<ResponseResult> ConfirmCourseRequest(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _courseRequestService.ConfirmCourseRequest(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);

        }

        #endregion

        #region ConfrimCourseRequest

        [Permission("NotConfirmCourseRequest")]
        public async Task<ResponseResult> NotConfirmCourseRequest(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _courseRequestService.NotConfirmCourseRequest(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);

        }

        #endregion


        [Permission("CourseRequestDetails")]
        public async Task<IActionResult> CourseRequestDetails(int id)
        {
            var result = await _courseRequestService.GetCourseRequestDetailsByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List), new { area = "Admin" });
            }

            return PartialView("_CourseRequestDetails", result.Value);

        }
    }
}
