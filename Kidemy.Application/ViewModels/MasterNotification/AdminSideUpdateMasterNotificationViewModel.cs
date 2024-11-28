using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.MasterNotification
{
    public class AdminSideUpdateMasterNotificationViewModel : BaseEntityViewModel<int>
    {

        [Display(Name = "Message")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Message { get; set; }

        [Display(Name = "DemoVideoFileName")]
        public string? DemoVideoFileName { get; set; }

    }
}
