using Kidemy.Domain.DTOs.Course;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Models.Course;
using System.Linq.Expressions;
using Barnamenevisan.Localizing.Repository;

namespace Kidemy.Domain.Interfaces.Course
{
    public interface ICourseRepository : IRepository<Models.Course.Course, int>
    {
        Task<int> GetCountOfAllCourses();

        Task<CourseDetailsModel?> GetCourseWithTagsAndCategoryNameById(string slug);

        Task<int> GetCourseIdBySlug(string slug);

        Task<List<CourseTitleModel>> GetCourseTitles(List<int> ids);

        Task<List<Models.Course.Course>> GetCoursesWithCategories(List<int> ids);

        Task<Models.Course.Course?> GetCourseByIdWithTagsAndCategories(int id);

        Task<int> GetRecordingCourseCountAsync();

        Task<Models.Course.Course?> GetCourseWithVideosAsync(string slug);

        Task<CourseVideo?> GetVideoAsync(int videoId);

        Task<CourseType> GetCourseType(int courseId);

        Task<List<CourseMasterModel>> GetMastersOfCourses(List<int> courseIds);

        Task<string> GetCourseSlugById(int courseId);

        Task<int> GetCourseMasterIdByCourseId(int courseId);

        Task<List<Domain.Models.Course.Course>> GetOfferedCourses();

        Task<List<CourseCategoryTitleModel>> GetCoursesCategoryWithId(List<int> ids);

        Task<List<Models.Course.Course>> GetCoursesList(Expression<Func<Domain.Models.Course.Course, bool>> where = null, int? take = null, Expression<Func<Domain.Models.Course.Course, object>>? orderBy = null, Expression<Func<Domain.Models.Course.Course, object>>? orderByDesc = null);
        Task<string?> GetCourseSuffixName(int courseId);
        Task<bool> IsOrderDuplicated(int order , int courseId);
        Task<List<CourseModel>> GetLastCourses(int take = 12);
    }
}
