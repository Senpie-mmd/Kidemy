using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Application.ViewModels.Notification
{
    public class FilterNotificationViewModel : BasePaging<AdminSideNotificationViewModel>
    {
        public string? Subject { get; set; }
    }
}
