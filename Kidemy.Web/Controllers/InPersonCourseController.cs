using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.InPersonCourse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class InPersonCourseController : BaseController
    {
        #region Fields
        private readonly IInPersonCourseService _inPersonCourseService;
        private readonly IStringLocalizer _localizer;
        private readonly ILogger<CoursesController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Ctor
        public InPersonCourseController(IInPersonCourseService inPersonCourseService,
            IStringLocalizer localizer,
            ILogger<CoursesController> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _inPersonCourseService = inPersonCourseService;
            _localizer = localizer;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion


        [HttpGet("inPerson-courses-list")]
        public async Task<IActionResult> InPersonCoursesList(ClientSideFilterInPersonCourseViewModel filter)
        {
            filter.TakeEntity = 9;
            var result = await _inPersonCourseService.FilterInPersonCoursesClientSide(filter);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(filter);
            }

            return View(filter);
        }
    }
}
