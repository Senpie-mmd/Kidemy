using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.MasterNotification
{
    public class MasterNotification : AuditBaseEntity<int>
    {
        public int SenderId { get; set; }
        public string Message { get; set; }
        public string? DemoVideoFileName { get; set; }


        #region Relations
        public ICollection<NotificationMasterMapping> Masters { get; set; }

        #endregion
    }
}
