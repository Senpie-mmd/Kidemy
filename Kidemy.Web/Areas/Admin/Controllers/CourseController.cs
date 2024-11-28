using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseVideos;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageCourses")]
    public class CourseController : BaseAdminController
    {
        #region Fields

        private readonly ICourseService _courseService;
        private readonly IMasterService _masterService;
        private readonly IStringLocalizer _localizer;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ICourseCommentService _courseCommentService;
        private readonly ICourseVideoService _videoService;

        #endregion

        #region Ctor
        public CourseController(ICourseService courseService,
            IMasterService masterService,
            IStringLocalizer localizer,
            ICourseCategoryService courseCategoryService,
            ICourseCommentService courseCommentService,
            ICourseVideoService videoService)
        {
            _courseService = courseService;
            _masterService = masterService;
            _localizer = localizer;
            _courseCategoryService = courseCategoryService;
            _courseCommentService = courseCommentService;
            _videoService = videoService;
        }
        #endregion

        #region Actions

        [Permission("CoursesList")]
        public async Task<IActionResult> CoursesList(AdminSideFilterCourseViewModel filter)
        {
            var result = await _courseService.FilterCoursesAdminSide(filter);

            return View(result.Value);
        }

        [Permission("DeleteCourse")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResponseResult> DeleteCourse(int id)
        {
            if (id < 1)
            {
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }
            var result = await _courseService.SoftDeleteCourseById(id);
            if (result.IsFailure)
            {
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return ResponseResult.Success();
        }

        #region CreateCourse

        [Permission("CreateNewCourse")]
        public async Task<IActionResult> CreateNewCourse()
        {
            AdminSideCreateCourseViewModel model = new AdminSideCreateCourseViewModel();

            var categories = await _courseCategoryService.GetCategoriesAsOptions();
            if (categories.IsFailure)
            {
                model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                return View(model);
            }
            model.Categories = categories.Value;
            //This is a commen for testing in git
            return View(model);
        }

        [Permission("CreateNewCourse")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewCourse(AdminSideCreateCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categoriesResult = await _courseCategoryService.GetCategoriesAsOptions();

                if (categoriesResult.IsFailure)
                {
                    model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                    return View(model);
                }

                model.Categories = categoriesResult.Value;
                return View(model);
            }

            var result = await _courseService.CreateNewCourse(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();

                var categoryResult = await _courseCategoryService.GetCategoriesAsOptions();
                if (categoryResult.IsFailure)
                {
                    model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                    return View(model);
                }

                model.Categories = categoryResult.Value;
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(CoursesList));
        }

        #endregion

        #region UpdateCourse

        [Permission("UpdateCourse")]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            if (id < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(CoursesList));
            }

            var result = await _courseService.GetCourseForEdit(id);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(CoursesList));
            }

            var categories = await _courseCategoryService.GetCategoriesAsOptions();
            if (categories.IsFailure)
            {
                result.Value.Categories = new List<AdminSideCategoryAsOptionViewModel>();

                return View(result.Value);
            }
            result.Value.Categories = categories.Value;

            return View(result.Value);
        }

        [Permission("UpdateCourse")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourse(AdminSideUpdateCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _courseCategoryService.GetCategoriesAsOptions();
                if (categories.IsFailure)
                {
                    model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                }
                else
                {
                    model.Categories = categories.Value;
                }

                return View(model);
            }
            var result = await _courseService.UpdateCourse(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                var categories = await _courseCategoryService.GetCategoriesAsOptions();
                if (categories.IsFailure)
                {
                    model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                }

                model.Categories = categories.Value;

                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(CoursesList));
        }

        #endregion

        #region LoadMasters

        [Permission("CreateNewCourse")]
        public async Task<IActionResult> LoadMastersForCourse(FilterForAdminSideMasterViewModel model)
        {
            var result = await _masterService.FilterForAdminSideAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();

                FilterForAdminSideMasterViewModel filter = new FilterForAdminSideMasterViewModel();
                return PartialView("_MastersList", filter);
            }

            return PartialView("_MastersList", result.Value);
        }

        #endregion

        #region CourseComments 
        [Permission("ConfirmOrDenyComment")]
        public async Task<IActionResult> LoadCourseComments(AdminSideFilterCourseCommentsViewModel filter)
        {
            if (filter.CourseId < 1) return PartialView("_CourseComments");

            var result = await _courseCommentService.FilterCommentsAdminSide(filter);
            if (result.IsFailure)
            {
                return PartialView("_CourseComments", new AdminSideFilterCourseCommentsViewModel());
            }

            return PartialView("_CourseComments", result.Value);
        }

        [Permission("ConfirmOrDenyComment")]
        public async Task<IActionResult> ConformCourseComment(int commentId,
            int currentPage = 1,
            CourseCommentsScore? CommentScore = null,
            bool? IsConfirmed = null,
            string? CommentMessage = null,
            int? UserId = null,
            string? UserName = null)
        {
            if (commentId < 1) return null;

            var result = await _courseCommentService.ConfirmOrDenyComment(commentId, true);
            if (result.IsFailure)
                return null;

            return RedirectToAction(nameof(LoadCourseComments),
                new
                {
                    CourseId = result.Value,
                    Page = currentPage,
                    CommentScore = CommentScore,
                    IsConfirmed = IsConfirmed,
                    CommentMessage = CommentMessage,
                    UserId = UserId,
                    UserName = UserName
                });
        }

        [Permission("ConfirmOrDenyComment")]
        public async Task<IActionResult> DenyCourseComment(int commentId,
            int currentPage = 1,
            CourseCommentsScore? CommentScore = null,
            bool? IsConfirmed = null,
            string? CommentMessage = null,
            int? UserId = null,
            string? UserName = null)
        {
            if (commentId < 1) return null;

            var result = await _courseCommentService.ConfirmOrDenyComment(commentId, false);
            if (result.IsFailure)
                return null;

            return RedirectToAction(nameof(LoadCourseComments),
                new
                {
                    CourseId = result.Value,
                    Page = currentPage,
                    CommentScore = CommentScore,
                    IsConfirmed = IsConfirmed,
                    CommentMessage = CommentMessage,
                    UserId = UserId,
                    UserName = UserName
                });
        }

        #endregion

        #region CourseVideos

        [Permission("CourseVideoList")]
        public async Task<IActionResult> CourseVideosList(AdminSideFilterCourseVideoViewModel filter)
        {
            if (filter?.CourseId < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(CoursesList));
            }

            var result = await _videoService.FilterCourseVideosCategory(filter);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(CoursesList));
            }

            return View(result.Value);
        }

        [Permission("DeleteCourseVideo")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourseVideo(int id)
        {
            if (id < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(CoursesList));
            }
            var result = await _videoService.DeleteCourseVideoById(id);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(CoursesList));
            }

            return RedirectToAction(nameof(CourseVideosList), new { CourseId = result.Value });
        }

        #region Publish

        [Permission("PublishCourseVideo")]
        [HttpGet]
        public async Task<IActionResult> PublishCourseVideo(int id, int courseId)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.NullValue].ToString();
                return RedirectToAction(nameof(CourseVideosList), new { area = "Admin", CourseId = courseId });
            }

            var result = await _videoService.PublishCourseVideoAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(CourseVideosList), new { area = "Admin", CourseId = courseId });
        }

        [Permission("PublishCourseVideo")]
        [HttpPost]
        public async Task<ResponseResult> PublishCourseVideoAjax(int id, int courseId)
        {
            if (id <= 0)
            {
                return ResponseResult.Failure(_localizer[ErrorMessages.NullValue].ToString());
            }

            var result = await _videoService.PublishCourseVideoAsync(id);

            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }
            else
            {
                return ResponseResult.Success(_localizer[result.Message].ToString());
            }
        }

        #endregion

        #region CreateCourseVideo

        [Permission("CreateNewCourseVideo")]
        public async Task<IActionResult> CreateNewCourseVideo(int courseId)
        {
            if (courseId < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(CoursesList));
            }

            var courseTypeResult = await _courseService.CheckIfCourseVideoCanBeFree(courseId);

            var videoCategoryResult = await _videoService.GetCourseVideoCategoryAsOptions(courseId);

            if (videoCategoryResult.IsFailure)
            {
                return View(new AdminSideCreateCourseVideoViewModel() { CourseId = courseId, CanShowIsFreeOption = courseTypeResult.Value });
            }
            var model = new AdminSideCreateCourseVideoViewModel() { VideoCategories = videoCategoryResult.Value, CourseId = courseId, CanShowIsFreeOption = courseTypeResult.Value };


            return View(model);
        }

        [Permission("CreateNewCourseVideo")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewCourseVideo(AdminSideCreateCourseVideoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var videoCategoryResult = await _videoService.GetCourseVideoCategoryAsOptions(model.CourseId);
                if (videoCategoryResult.IsFailure)
                    return View(model);

                model.VideoCategories = videoCategoryResult.Value;
                return View(model);
            }

            var result = await _videoService.CreateNewCourseVideo(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();

                var videoCategoryResult = await _videoService.GetCourseVideoCategoryAsOptions(model.CourseId);
                if (videoCategoryResult.IsFailure)
                    return View(model);

                model.VideoCategories = videoCategoryResult.Value;
                return View(model);
            }
            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(CourseVideosList), new { CourseId = model.CourseId });
        }

        #endregion

        #region UpdateCourseVideo 

        [Permission("UpdateCourseVideo")]
        public async Task<IActionResult> UpdateCourseVideo(int id)
        {
            if (id < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(CoursesList));
            }
            var result = await _videoService.GetCourseVideoForEdit(id);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(CoursesList));
            }

            var CanChangeIsFreeResult = await _courseService.CheckIfCourseVideoCanBeFree(result.Value.CourseId);
            result.Value.CanChageIsFree = CanChangeIsFreeResult.Value;

            var videoCategoryResult = await _videoService.GetCourseVideoCategoryAsOptions(result.Value.CourseId);
            if (videoCategoryResult.IsFailure)
            {
                return View(result.Value);
            }
            result.Value.VideoCategories = videoCategoryResult.Value;

            return View(result.Value);
        }

        [Permission("UpdateCourseVideo")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourseVideo(AdminSideUpdateCourseVideoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var videoCategoryResult = await _videoService.GetCourseVideoCategoryAsOptions(model.CourseId);
                if (videoCategoryResult.IsFailure)
                    return View(model);

                model.VideoCategories = videoCategoryResult.Value;
                return View(model);
            }
            var result = await _videoService.UpdateCourseVideo(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                var videoCategoryResult = await _videoService.GetCourseVideoCategoryAsOptions(model.CourseId);
                if (videoCategoryResult.IsFailure)
                    return View(model);

                model.VideoCategories = videoCategoryResult.Value;
                return View(model);
            }
            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(CourseVideosList), new { CourseId = model.CourseId });
        }
        #endregion

        #endregion

        #endregion
    }
}