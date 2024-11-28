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
    public class AdminSideMasterNotificationViewModel : BaseEntityViewModel<int>
    {
        public int SenderId { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Message { get; set; }

        [Display(Name = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "DemoVideoFileName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string DemoVideoFileName { get; set; }

        public string? FileNameSuffix { get; set; }

        public List<int>  MasterIds { get; set; }
    }
}
