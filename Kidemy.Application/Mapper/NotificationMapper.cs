using Kidemy.Application.ViewModels.Notification;
using Kidemy.Domain.Models.Notification;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class NotificationMapper
    {
        public static Expression<Func<Notification, AdminSideNotificationViewModel>> AdminMapNotificationViewModel => (Notification notification) => new AdminSideNotificationViewModel
        {
            Id = notification.Id,
            Subject = notification.Subject,
            Message = notification.Message,
            SenderId = notification.SenderId,
        };

        public static AdminSideUpdateNotificationViewModel MapFrom(this AdminSideUpdateNotificationViewModel model, Notification notification)
        {
            model.Id = notification.Id;
            model.Subject = notification.Subject;
            model.Message = notification.Message;

            return model;
        }
    }
}
