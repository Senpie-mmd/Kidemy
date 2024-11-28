using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.Notification
{
    public record NotificationDeletedEvent
   (
     int Id) : INotification;
}
