using Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ICourseCategoryService
    {
        Task<Result<List<AdminSideCategoryAsOptionViewModel>>> GetCategoriesAsOptions(int? excludeCourseCategoryId = null);

        Task<Result> CreateNewCategory(AdminSideCreateCategoryViewModel model);

        Task<Result<AdminSideFilterCategoryViewModel>> FilterCategoryListAsync(AdminSideFilterCategoryViewModel model);

        Task<Result> DeleteCategory(int id);

        Task<Result<AdminSideUpdateCategoryViewModel>> GetCourseCategoryForEdit(int id);

        Task<Result> UpdateCourseCategory(AdminSideUpdateCategoryViewModel model);

        Task<Result<List<ClientSideCourseCategoriesLinkInNavViewModel>>> GetCategoriesForNav();
    }
}
