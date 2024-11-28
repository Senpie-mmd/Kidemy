using Kidemy.Application.ViewModels.Master;
using Kidemy.Application.ViewModels.MasterNotification;
using Kidemy.Application.ViewModels.Notification;
using Kidemy.Application.ViewModels.Slider;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IMasterNotificationService
    {
        Task<Result> SendMasterNotification(SendMasterNotificationViewModel notification);
        Task<Result<List<ClientSideMasterNotificationViewModel>>> GetMasterNotificationsAsync(int masterId);
        Task<Result<FilterMasterNotificationViewModel>> FilterAsync(FilterMasterNotificationViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result> UpdateAsync(AdminSideUpdateMasterNotificationViewModel model);
        Task<Result<AdminSideUpdateMasterNotificationViewModel>> GetMasterNotificationForEditById(int id);
    }
}
