using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.CourseRequest;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class CourseRequestRegisterViewComponent : ViewComponent
    {
        #region Fields

        private readonly ICourseRequestService _courseRequestService;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        #endregion

        #region Ctor

        public CourseRequestRegisterViewComponent(ICourseRequestService courseRequestService, IUserService userService, ICourseService courseService)
        {
            _courseRequestService = courseRequestService;
            _userService = userService;
            _courseService = courseService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(ClientSideCourseRequestRegisterViewModel model)
        {
            var mastersList = await _userService.GetMastersListAsync();

            if (mastersList.IsSuccess)
            {
                ViewData["MastersList"] = mastersList.Value;
            }

            var courseTagsList = await _courseService.GetCourseTagsList();

            if (courseTagsList.IsSuccess)
            {
                ViewData["CourseTagsList"] = courseTagsList.Value;
            }

            return View("CourseRequestRegister", model);
        }
    }
}
