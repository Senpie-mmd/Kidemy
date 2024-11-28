using Kidemy.Application.ViewModels.Course;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseNotification;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseTag;
using Kidemy.Application.ViewModels.Course.UserPanel;
using Kidemy.Application.ViewModels.FavouriteCourse;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Domain.DTOs.Course;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Enums.CourseNotification;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ICourseService
    {
        Task<Result<List<ClientSideCategoryViewModel>>> GetCategoriesListClientSide();

        Task<Result<ClientSideFilterCoursesViewModel>> FilterCoursesForClientSide(ClientSideFilterCoursesViewModel filter);

        Task<Result<ClientSideFilterCoursesViewModel>> GetDiscountedCoursesOfTheDay(ClientSideFilterCoursesViewModel filter);

        Task<Result<int>> GetCourseCount();

        Task<Result<ClientSideCourseDetailViewModel>> GetCourseDetailClienSide(string slug);

        Task<Result<ClientSideCourseWithVideosViewModel>> GetCourseVideosByCourseSlug(string slug);

        Task<Result<ClientSideFilterCommentsViewModel>> GetCommentsForClientSide(string slug, int page = 1);

        Task<Result<string>> CreateComment(ClientSideCreateCommentViewModel model);

        Task<Result<List<CourseTitleModel>>> GetCourseTitles(List<int> ids);

        Task<Result<List<CourseCategoryTitleModel>>> GetCourseCategoryTitles(List<int> ids);

        Task<Result<AdminSideFilterCourseViewModel>> FilterCoursesAdminSide(AdminSideFilterCourseViewModel filter);

        Task<Result<bool>> IsThereAnyCourseWithThisCategoryId(int id);

        Task<int> GetCourseIdBySlug(string slug);

        Task<Result<List<CourseFullDetailsViewModel>>> GetCoursesFullDetailsByIdsAsync(List<int> ids);

        Task<Result<List<CourseCategoryWithSubCategoriesViewModel>>> GetCategoriesWithSubCategoriesAsync(List<int> courseCategoryIds);

        Task<Result> CreateNewCourse(AdminSideCreateCourseViewModel model);

        Task<Result> SoftDeleteCourseById(int id);

        Task<Result<List<ClientSideCourseTagViewModel>>> GetCourseTagsList();

        Task<Result<AdminSideUpdateCourseViewModel>> GetCourseForEdit(int id);

        Task<Result> UpdateCourse(AdminSideUpdateCourseViewModel model);

        Task<Result> UpdateCourseView(int courseId);

        Task<Result<bool>> IsCommentsOpen(string slug);

        Task<Result<List<ClientSideCourseShortDetailsViewModel>>> GetCoursesForClientByIdsAsync(List<int> ids);

        Task<Result<bool>> UserHasCourseAsync(int courseId, int userId);

        Task<Result<ClientSideCourseShortDetailsViewModel>> GetCourseForClientByIdAsync(int id);

        Task<Result<List<AdminSideCourseViewModel>>> GetCoursesForAdminByIdsAsync(List<int> ids);

        Task<Result> AddCoursesForUser(int userId, List<int> courseIds);

        Task<Result<List<CourseShortDetailsViewModel>>> GetCoursesShortDetailsByIdsAsync(List<int> ids);

        Task<Result<List<ClientSideHomePageOfferedCoursesViewModel>>> GetSuggetionCourse(int count);

        Task<Result<ClientSideFilterCourseForUserPanelViewModel>> FilterCoursesOfUser(ClientSideFilterCourseForUserPanelViewModel filter);

        Task<Result<ClientSideMasterDetailsViewModel>> GetMasterByCourseSlugAsync(string slug);

        Task<Result<FilterUserPanelCoursesViewModel>> UserPanelFilterCourses(FilterUserPanelCoursesViewModel filter);

        Task<Result<string>> GetCourseSlugById(int courseId);

        Task<Result<CourseVideoTitleViewModel>> GetVideoFileNameForUser(int courseId, int videoId, int userId);

        Task<Result<List<CourseMasterModel>>> GetMastersOfCourses(List<int> courseIds);

        Task<Result<int>> GetRecordingCourseCountAsync();


        Task<Result<List<ClientSideLastCoursesViewModel>>> GetLastCourses();

        Task<Result<bool>> CheckIfCourseVideoCanBeFree(int courseId);

        Task<Result<CourseType>> GetCourseType(int courseId);

        #region Add And Remove Favourite
        Task<Result<bool>> SetFavouriteCoursesAsync(ClientSideFavouriteCourseViewModel model);
        Task<Result<ClientSideFilterFavouriteCourseViewModel>> FilterUserFavouriteCoursesAsync(ClientSideFilterFavouriteCourseViewModel model);
        Task<Result<List<int>>> GetAllUserFavouriteCoursesAsync(int userId);
        Task<Result<List<ClientSideGetSlugsViewModel>>> GetCourseSlugsByUserIds(List<int> ids);
        Task<Result> AddFreeCourseToUserCourses(int courseId, int userId);

        #endregion

        #region Course Notification

        Task<Result<List<int>>> GetAllUsersNotificationByCourseIdAsync(int courseId, CourseNotificationType type);
        Task<Result> AddUserForCourseNotificationAsync(ClientSideAddCourseNotificationViewModel model);
        Task<Result<bool>> UserCanPlayAllVideosOfCourse(int courseId, int userId);
        Task<Result<List<ClientSideCourseViewModel>>> GetLastCoursesForHomePage();
        Task<Result<AdminSideCourseViewModel>> GetCourseById(int courseId);
        Task<Result<List<ClientSideMastersOtherCoursesViewModel>>> GetOfferedCoursesForMaster(int masterId, int currentCourseId);
        Task<Result<List<ClientSideMainPagePopularCoursesViewModel>>> GetPopularCourses();

        #endregion
    }
}
