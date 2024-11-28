using MediatR;

namespace Kidemy.Domain.Events.Notification
{
    public record NotificationUpdatedEvent
      (
      int Id,
      string NewMessage,
      string OldMessage

      ) : INotification;
}
