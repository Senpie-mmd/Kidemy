using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class CategoriesLinkInNavViewComponent : ViewComponent
    {
        #region Fields
        private readonly ICourseCategoryService _categoryService;
        #endregion

        #region Ctor
        public CategoriesLinkInNavViewComponent(ICourseCategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _categoryService.GetCategoriesForNav();
            if (result.IsFailure)
            {
                return View("CategoriesLinkInNav", new List<ClientSideCourseCategoriesLinkInNavViewModel>());
            }

            return View("CategoriesLinkInNav", result.Value);
        }
    }
}
