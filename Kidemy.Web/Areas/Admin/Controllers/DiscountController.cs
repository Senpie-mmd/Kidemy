using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Course;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.Discount.AdminSide;
using Kidemy.Application.ViewModels.User.AdminSide;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageDiscount")]
    public class DiscountController : BaseAdminController
    {
        #region Fields

        private readonly IDiscountService _discountService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ILocalizingService _localizingService;
        private readonly IUserService _userService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ICourseService _courseService;

        #endregion

        #region Constructor

        public DiscountController(IDiscountService discountService, IStringLocalizer<SharedResource> localizer, ILocalizingService localizingService, IUserService userService, ICourseCategoryService courseCategoryService, ICourseService courseService)
        {
            _discountService = discountService;
            _localizer = localizer;
            _localizingService = localizingService;
            _userService = userService;
            _courseCategoryService = courseCategoryService;
            _courseService = courseService;
        }

        #endregion

        #region Actions

        #region Discount

        #region List

        [Permission("DiscountList")]
        public async Task<IActionResult> List(AdminSideFilterDiscountViewModel model)
        {
            var result = await _discountService.FilterForAdminAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(result.Value);
        }

        #endregion

        #region Create

        [Permission("CreateDiscount")]
        public IActionResult Create()
        {
            return View(new AdminSideUpsertDiscountViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permission("CreateDiscount")]
        public async Task<IActionResult> Create(AdminSideUpsertDiscountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _discountService.CreateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Update

        [Permission("UpdateDiscount")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _discountService.GetByIdForEditByAdminAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List));
            }

            return View(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permission("UpdateDiscount")]
        public async Task<IActionResult> Update(AdminSideUpsertDiscountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _discountService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Delete

        [Permission("DeleteDiscount")]
        public async Task<ResponseResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return new ResponseResult(false);
            }

            var result = await _discountService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return new ResponseResult(false);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return new ResponseResult(true);
        }

        #endregion

        #endregion

        #region DiscountLimitation

        #region List

        [Permission("DiscountLimitationList")]
        public async Task<IActionResult> DiscountLimitationList(AdminSideFilterDiscountLimitationViewModel model)
        {
            var result = await _discountService.FilterDiscountLimitationAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(result.Value);
        }

        #endregion

        #region Create

        [Permission("CreateDiscountLimitation")]
        public async Task<IActionResult> CreateDiscountLimitation(int discountId)
        {
            return PartialView(new AdminSideUpsertDiscountLimitationViewModel() { DiscountId = discountId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permission("CreateDiscountLimitation")]
        public async Task<IActionResult> CreateDiscountLimitation(AdminSideUpsertDiscountLimitationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState
                    .Values
                    .FirstOrDefault(x => x.Errors.Count > 0)?
                    .Errors
                    .FirstOrDefault()?
                    .ErrorMessage;

                TempData[ErrorMessage] = message;
                return RedirectToAction(nameof(DiscountLimitationList), new { discountId = model.DiscountId });
            }

            var result = await _discountService.CreateDiscountLimitationAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(DiscountLimitationList), new { discountId = model.DiscountId });
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(DiscountLimitationList), new { discountId = model.DiscountId });
        }

        #endregion

        #region Update

        [Permission("UpdateDiscountLimitation")]
        public async Task<IActionResult> UpdateDiscountLimitation(int id)
        {
            var result = await _discountService.GetDiscountLimitationForUpdateByAdmin(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(DiscountLimitationList));
            }

            return PartialView(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permission("UpdateDiscountLimitation")]
        public async Task<IActionResult> UpdateDiscountLimitation(AdminSideUpsertDiscountLimitationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState
                    .Values
                    .FirstOrDefault(x => x.Errors.Count > 0)?
                    .Errors
                    .FirstOrDefault()?
                    .ErrorMessage;

                TempData[ErrorMessage] = message;
                return RedirectToAction(nameof(DiscountLimitationList), new { discountId = model.DiscountId });
            }

            var result = await _discountService.UpdateDiscountLimitationAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(DiscountLimitationList), new { discountId = model.DiscountId });
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction(nameof(DiscountLimitationList), new { discountId = model.DiscountId });
        }

        #endregion

        #region Delete

        [Permission("DeleteDiscountLimitation")]
        public async Task<ResponseResult> DeleteDiscountLimitation(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return new ResponseResult(false);
            }

            var result = await _discountService.DeleteDiscountLimitationAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return new ResponseResult(false);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return new ResponseResult(true);
        }

        #endregion

        #endregion

        #region FilterUsers

        public async Task<IActionResult> FilterUsers(FilterUsersViewModel model)
        {
            var result = await _userService.FilterUsersAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return PartialView("_FilterUsers", result.Value);
        }

        #endregion

        #region FilterCategories

        public async Task<IActionResult> FilterCategories(AdminSideFilterCategoryViewModel model)
        {
            var result = await _courseCategoryService.FilterCategoryListAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return PartialView("_FilterCategories", result.Value);
        }

        #endregion

        #region FilterCourses

        public async Task<IActionResult> FilterCourses(AdminSideFilterCourseViewModel model)
        {
            if (model.PriceType == null)
            {
                model.PriceType = CourseType.Mercenary;
            }

            var result = await _courseService.FilterCoursesAdminSide(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return PartialView("_FilterCourses", result.Value);
        }

        #endregion

        #endregion
    }
}
