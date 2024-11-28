using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Link;
using Kidemy.Domain.Interfaces.Notification;
using Kidemy.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.Notification
{
    public class NotificationRepository : EfRepository<Domain.Models.Notification.Notification, int>, INotificationRepository
    {
        #region Constructor
        public NotificationRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
