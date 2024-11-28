using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Site;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class CounterViewComponent : ViewComponent
    {
        #region Fields

        private readonly IDynamicTextService _dynamicTextService;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        #endregion

        #region Ctor

        public CounterViewComponent(ICourseService courseService, IUserService userService, IDynamicTextService dynamicTextService)
        {
            _courseService = courseService;
            _userService = userService;
            _dynamicTextService = dynamicTextService;
        }


        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userCount = (await _dynamicTextService.GetDynamicTextByPosition(Domain.Enums.DynamicText.DynamicTextPosition.UserCount)).Value.Text;

            var courseCount = (await _courseService.GetCourseCount()).Value;

            var mastersCount= (await _userService.GetMastersCountAsync()).Value;

            var recordingCourseCount = (await _courseService.GetRecordingCourseCountAsync()).Value;

            var model = new ClientSideCounterViewModel
            {
                UserCount = userCount,
                CourseCount = courseCount,
                MastersCount = mastersCount,
                RecordingCourseCount = recordingCourseCount
            };

            return View("Counter",model);
        }
    }
}
