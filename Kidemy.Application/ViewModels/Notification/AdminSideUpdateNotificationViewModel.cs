using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.Notification
{
    public class AdminSideUpdateNotificationViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

    }
}
