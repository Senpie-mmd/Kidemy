using Barnamenevisan.Localizing.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.Notification
{
    public class NotificationUserMapping 
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }

        #region Relations

        public Notification Notification { get; set; }

        #endregion
    }
}
