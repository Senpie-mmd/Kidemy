using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Consultation
{
    public enum ConsultationRequestState
    {

        [Display(Name = "WaitingForPayment")]
        WaitingForPayment,

        [Display(Name = "WaitingForSetTime")]
        WaitingForSetTime,

        [Display(Name = "WaitingForEvent")]
        WaitingForEvent,

        [Display(Name = "Finished")]
        Finished,

        [Display(Name = "Canceled")]
        Canceled
    }
}
