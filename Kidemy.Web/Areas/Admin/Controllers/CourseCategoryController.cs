using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageCourseCategories")]
    public class CourseCategoryController : BaseAdminController
    {
        #region Fields
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly IStringLocalizer _localizer;
        #endregion

        #region Ctor
        public CourseCategoryController(ICourseCategoryService courseCategoryService,
            IStringLocalizer localizer)
        {
            _courseCategoryService = courseCategoryService;
            _localizer = localizer;
        }
        #endregion

        #region Actions
        [Permission("CourseCategories")]
        public async Task<IActionResult> CategoriesList(AdminSideFilterCategoryViewModel filter)
        {
            var result = await _courseCategoryService.FilterCategoryListAsync(filter);

            return View(result.Value);
        }


        [Permission("CreateNewCourseCategory")]
        public async Task<IActionResult> CreateNewCategory()
        {
            AdminSideCreateCategoryViewModel model = new AdminSideCreateCategoryViewModel();
            var categories = await _courseCategoryService.GetCategoriesAsOptions();
            if (categories.IsFailure)
            {
                model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                return View(model);
            }
            model.Categories = categories.Value;
            return View(model);
        }


        [Permission("CreateNewCourseCategory")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewCategory(AdminSideCreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _courseCategoryService.GetCategoriesAsOptions();
                if (categories.IsFailure)
                {
                    model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                    return View(model);
                }
                model.Categories = categories.Value;
                return View(model);
            }

            var result = await _courseCategoryService.CreateNewCategory(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                var categories = await _courseCategoryService.GetCategoriesAsOptions();
                if (categories.IsFailure)
                {
                    model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                    return View(model);
                }
                model.Categories = categories.Value;
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(CategoriesList));
        }


        [Permission("DeleteCourseCategory")]
        public async Task<ResponseResult> DeleteCategory(int id)
        {
            if (id < 1)
            {
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }
            var result = await _courseCategoryService.DeleteCategory(id);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return ResponseResult.Failure(result.Message);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
                return ResponseResult.Success(result.Message);
            }
        }


        [Permission("UpdateCourseCategory")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            if (id < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(CategoriesList));
            }
            var result = await _courseCategoryService.GetCourseCategoryForEdit(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(CategoriesList));
            }

            var categories = await _courseCategoryService.GetCategoriesAsOptions(id);

            if (categories.IsFailure)
            {
                result.Value.Categories = new List<AdminSideCategoryAsOptionViewModel>();
            }
            else
            {
                result.Value.Categories = categories.Value;
            }

            return View(result.Value);
        }


        [Permission("UpdateCourseCategory")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(AdminSideUpdateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categoriesResult = await _courseCategoryService.GetCategoriesAsOptions(model.Id);
                if (categoriesResult.IsFailure)
                {
                    model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                }
                else
                {
                    model.Categories = categoriesResult.Value;
                }
                return View(model);
            }

            var result = await _courseCategoryService.UpdateCourseCategory(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();

                var categoriesResult = await _courseCategoryService.GetCategoriesAsOptions(model.Id);
                if (categoriesResult.IsFailure)
                {
                    model.Categories = new List<AdminSideCategoryAsOptionViewModel>();
                }
                else
                {
                    model.Categories = categoriesResult.Value;
                }
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction(nameof(CategoriesList));
        }
        #endregion
    }
}
