using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ICourseQuestionService
    {

        Task<Result<AdminSideFilterCourseQuestionViewModel>> FilterAsync(AdminSideFilterCourseQuestionViewModel model);
        Task<Result<AdminSideFilterCourseQuestionViewModel>> FilterNotConfirmedAsync(AdminSideFilterCourseQuestionViewModel model);
        Task<Result<AdminSideFilterCourseQuestionViewModel>> FilterAdminDashboardCourseQuestionsDoesNotAnsweredByTeacherAfter48HoursAsync(AdminSideFilterCourseQuestionViewModel model);

        Task<Result> CreateAsync(ClientSideUpsertCourseQuestionViewModel model);
        Task<Result> UpdateAsync(ClientSideUpsertCourseQuestionViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result> ChangeUserFeedBack(int id, bool feedBack);
        Task<Result> CloseCourseQuestionAsync(int id);
        Task<Result> OpenCourseQuestionAsync(int id);
        Task<Result> ConfrimCourseQuestion(int id);

        Task<Result<ClientSideFilterCourseQuestionViewModel>> FilterCourseQuestionsBySlug(ClientSideFilterCourseQuestionViewModel model);
        Task<Result<ClientSideFilterCourseQuestionViewModel>> FilterCourseQuestionsByUserId(ClientSideFilterCourseQuestionViewModel model);
        Task<Result<ClientSideFilterCourseQuestionViewModel>> FilterNotAnsweredCourseQuestionsByMasterId(ClientSideFilterCourseQuestionViewModel model);
        Task<Result<ClientSideCourseQuestionViewModel>> GetCourseQuestionsById(int id);

        Task<Result> CloseCourseQuestionAfter1MonthAsync();




    }
}
