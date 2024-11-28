using Kidemy.Application.ViewModels.Notification;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface INotificationService
    {
        Task<Result> SendNotification(SendNotificationViewModel notification);
        Task<Result<FilterNotificationViewModel>> FilterAsync(FilterNotificationViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result> UpdateAsync(AdminSideUpdateNotificationViewModel model);
        Task<Result<AdminSideUpdateNotificationViewModel>> GetNotificationForEditById(int id);
    }
}
