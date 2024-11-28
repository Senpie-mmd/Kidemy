using Kidemy.Application.ViewModels.InPersonCourse;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IInPersonCourseService
    {
        Task<Result<ClientSideFilterInPersonCourseViewModel>> FilterInPersonCoursesClientSide(ClientSideFilterInPersonCourseViewModel filter);
    }
}
