using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Sms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.SmsProvider
{
    public class SmsProviderDetailsViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "ApiKey")]
        public string ApiKey { get; set; }

        [Display(Name = "SmsProviderType")]
        public SmsProviderType SmsProviderType { get; set; }

        [Display(Name = "IsDefault")]
        public bool IsDefault { get; set; }

    }
}
