using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Sms
{
    public enum SmsProviderType
    {
        [Display(Name = "ParsGreen")]
        ParsGreen,

        [Display(Name = "KaveNegar")]
        KaveNegar,
    }
}
