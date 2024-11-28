using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Sms;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.SmsProvider
{
    public class AdminSideUpsertSmsProviderViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "ApiKey")]
        public string ApiKey { get; set; }

        [Display(Name = "SmsProviderType")]
        public SmsProviderType SmsProviderType { get; set; }

        [Display(Name = "IsDefault")]
        public bool IsDefault { get; set; }
    }
}
