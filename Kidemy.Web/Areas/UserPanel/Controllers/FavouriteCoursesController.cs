using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.FavouriteCourse;
using Kidemy.Domain.Shared;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class FavouriteCoursesController : BaseUserPanelController
    {
        #region Fields
        private readonly ICourseService _courseService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor
        public FavouriteCoursesController(ICourseService courseService, IStringLocalizer<SharedResource> localizer)
        {
            _courseService = courseService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [HttpGet("/userpanel/favourite-courses")]
        public async Task<IActionResult> List(ClientSideFilterFavouriteCourseViewModel model)
        {
            model.UserId = User.GetUserId();
            var courses = await _courseService.FilterUserFavouriteCoursesAsync(model);
            return View(courses.Value);
        }
        #endregion

        #region Remove

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return RedirectToAction(nameof(List), new { area = "UserPanel" });
            }
            var favouriteViewModel = new ClientSideFavouriteCourseViewModel()
            {
                CourseId = id,
                UserId = User.GetUserId(),
            };
            var result = await _courseService.SetFavouriteCoursesAsync(favouriteViewModel);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            }

            return RedirectToAction(nameof(List));
        }
        

        #endregion

        #endregion
    }
}
