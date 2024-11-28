using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Events.Consultation.ConsultationRequest
{
    public enum FilterConsultationRequestState
    {
        [Display(Name = "All")]
        All,

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
