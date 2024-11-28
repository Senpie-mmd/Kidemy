using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Domain.Models.Notification
{
    public class Notification : AuditBaseEntity<int>
    {
        public int SenderId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        #region Relations
        public ICollection<NotificationUserMapping> Users { get; set; }

        #endregion
    }
}
