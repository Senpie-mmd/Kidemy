using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.Notification
{
    public record NotificationCreatedEvent(
        string title,
        string message,
        List<int> userId
        ) : INotification;

}
