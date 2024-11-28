using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.MasterNotification;
using Kidemy.Domain.Interfaces.Notification;
using Kidemy.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.MasterNotification
{
    public class MasterNotificationRepository : EfRepository<Domain.Models.MasterNotification.MasterNotification, int>, IMasterNotificationRepository
    {
        #region Constructor
        public MasterNotificationRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
