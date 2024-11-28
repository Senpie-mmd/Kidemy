using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.CourseRequest;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class CourseRequestController : BaseController
    {
        #region Fields

        private readonly ICourseRequestService _courseRequestService;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IEncryptService _encryptService;

        #endregion

        #region Constructor

        public CourseRequestController(
            ICourseRequestService courseRequestService,
            IUserService userService,
            IStringLocalizer<SharedResource> localizer,
            IEncryptService encryptService)
        {
            _courseRequestService = courseRequestService;
            _userService = userService;
            _localizer = localizer;
            _encryptService = encryptService;
        }

        #endregion

        #region Actions

        #region CourseRequestList
        public async Task<IActionResult> CourseRequestList(FilterForClientSideCourseRequestViewModel filter)
        {
            var userId = HttpContext.User.GetUserId();

            var result = await _courseRequestService.FilterForClientSideAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(filter);
            }

            var userIsMasterResult = await _userService.UserIsMasterAsync(userId);

            if (userIsMasterResult.IsSuccess)
            {
                ViewData["UserIsMaster"] = userIsMasterResult.Value;
            }

            return View(result.Value);
        }

        #endregion

        #region CourseRequestRegister
        [Authorize]
        [HttpPost("/register-courseRequest"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CourseRequestRegister(ClientSideCourseRequestRegisterViewModel model)
        {
            model.RequestedById = HttpContext.User.GetUserId();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerResult = await _courseRequestService.CreateCourseRequestAsync(model);

            if (registerResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[registerResult.Message].ToString();
                return View(model);
            }
            TempData[SuccessMessage] = _localizer[registerResult.Message].ToString();
                 
            return RedirectToAction(nameof(CourseRequestList));
        }
        #endregion

        #region CourseRequestVoteRegister

        [Authorize]
        [HttpPost("/register-vote"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CourseRequestVoteRegister(ClientSideCourseRequestVoteRegisterViewModel model)
        {
            model.UserId = HttpContext.User.GetUserId();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerResult = await _courseRequestService.CreateCourseRequestVoteAsync(model);

            if (registerResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[registerResult.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[registerResult.Message].ToString();
            }

            return RedirectToAction(nameof(CourseRequestList));
        }

        #endregion

        #region CourseRequestMasterVolunteerRegister

        [Authorize]
        [HttpPost("/register-masterVolunteer"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CourseRequestMasterVolunteerRegister(ClientSideCourseRequestMasterVolunteerRegisterViewModel model)
        {

            model.MasterId = HttpContext.User.GetUserId();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerResult = await _courseRequestService.CreateCourseRequestMasterVolunteerAsync(model);

            if (registerResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[registerResult.Message].ToString();
                return RedirectToAction(nameof(CourseRequestList));
            }
            else
            {
                TempData[SuccessMessage] = _localizer[registerResult.Message].ToString();
            }

            return RedirectToAction(nameof(CourseRequestList));
        }

        #endregion 

        #endregion

    }
}
