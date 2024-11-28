using Barnamenevisan.Localizing.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.MasterNotification
{
    public class NotificationMasterMapping : AuditBaseEntity<int>
    {
        public int MasterNotificationId { get; set; }
        public int MasterId { get; set; }

        #region Relations

        public MasterNotification MasterNotification { get; set; }

        #endregion
    }
}
