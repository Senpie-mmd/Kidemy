using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.MasterNotification
{
    public class SendMasterNotificationViewModel
    {
        public int SenderId { get; set; }
        public string MasterIds { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Message { get; set; }

        [Display(Name = "DemoVideoFileName")]
        public string? DemoVideoFileName { get; set; }
        public bool IsAllMaster { get; set; } = false;
    }
}
