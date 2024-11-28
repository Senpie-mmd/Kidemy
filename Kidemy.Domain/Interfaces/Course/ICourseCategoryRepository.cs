using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.DTOs.Course;
using Kidemy.Domain.Models.Course;
using System.Linq.Expressions;

namespace Kidemy.Domain.Interfaces.Course
{
    public interface ICourseCategoryRepository : IRepository<CourseCategory, int>
    {
        Task<List<CategoryModel>> GetCategoriesWithCountOfCourses(Expression<Func<CourseCategory, bool>> where = null);
        Task<List<CourseCategory>> GetCategoriesWithSubCategories(List<int> ids);
        Task<List<CourseCategoryTitleModel>> GetCourseCategoryTitles(List<int> ids);

        Task<List<PopularCourseCategoriesModel>> GetPopularCategoriesWithCourses();
    }
}
