using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestionAnswer;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestionAnswer;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ICourseQuestionAnswerService
    {
        Task<Result> CreateAsync(ClientSideUpsertCourseQuestionAnswerViewModel model);

        Task<Result<ClientSideFilterCourseQuestionAnswerViewModel>> GetCourseQuestionAnswersByQuestionIdForClientAsync(ClientSideFilterCourseQuestionAnswerViewModel model);
        Task<Result<AdminSideFilterCourseQuestionAnswerViewModel>> GetCourseQuestionAnswersByQuestionIdForAdminAsync(int questionId);
        Task<Result<ClientSideFilterCourseQuestionAnswerViewModel>> GetCourseQuestionAnswersByUserIdClientUserPanelAsync(ClientSideFilterCourseQuestionAnswerViewModel model);
        Task<Result<int>> DeleteAsync(int id);
        Task<Result> ConfirmAsync(int id);
    }
}
