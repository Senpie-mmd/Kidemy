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
    public class AdminSideNotificationViewModel : BaseEntityViewModel<int>
    {
        public int SenderId { get; set; }
        public string UserIds { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Message { get; set; }
        public bool IsAllUser { get; set; } = false;
    }
}
