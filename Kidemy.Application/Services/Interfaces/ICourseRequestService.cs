using Kidemy.Application.ViewModels.CourseRequest;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ICourseRequestService
    {
        Task<Result> CreateCourseRequestAsync(ClientSideCourseRequestRegisterViewModel model);
        Task<Result<FilterForClientSideCourseRequestViewModel>> FilterForClientSideAsync(FilterForClientSideCourseRequestViewModel model);
        Task<Result> CreateCourseRequestVoteAsync(ClientSideCourseRequestVoteRegisterViewModel model);
        Task<Result> CreateCourseRequestMasterVolunteerAsync(ClientSideCourseRequestMasterVolunteerRegisterViewModel model);
        Task<Result<FilterForAdminSideCourseRequestViewModel>> FilterForAdminSideAsync(FilterForAdminSideCourseRequestViewModel model);
        Task<Result> ConfirmCourseRequest(int id);
        Task<Result> NotConfirmCourseRequest(int id);
        Task<Result<AdminSideCoureRequestDetailsViewModel>> GetCourseRequestDetailsByIdAsync(int id);
        Task<Result<int>> GetCountAsync(bool isConfirm);
    }
}
