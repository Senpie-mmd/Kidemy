using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Sms;

namespace Kidemy.Domain.Models.SmsProvider
{
    public class SmsProvider : BaseEntity<int>
    {
        public string ApiKey { get; set; }

        public SmsProviderType SmsProviderType { get; set; }

        public bool IsDefault { get; set; }

    }
}
