using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.ZarinPalPayment
{
    public class ZarinPalPaymentCreatResponseModel
    {
        public string Authoriy { get; set; }
    }
    public enum ZarinPalPaymentStatus
    {
        [Display(Name = "St1")]
        St1 = -1,

        [Display(Name = "St3")]
        St3 = -3,

        [Display(Name = "St11")]
        St11 = -11,

        [Display(Name = "St21")]
        St21 = -21,

        [Display(Name = "St22")]
        St22 = -22,

        [Display(Name = "St33")]
        St33 = -33,

        [Display(Name = "St100")]
        St100 = 100,

        [Display(Name = "St101")]
        St101 = 101,
    }
}
