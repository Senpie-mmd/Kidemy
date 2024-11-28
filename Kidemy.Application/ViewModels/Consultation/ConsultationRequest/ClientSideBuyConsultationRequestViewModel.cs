namespace Kidemy.Application.ViewModels.Consultation.ConsultationRequest
{
    public class ClientSideBuyConsultationRequestViewModel
    {
        public int? UserId { get; set; }
        public string? UserIP { get; set; }

        public int ConsultationRequestId { get; set; }
        public bool UseFromWalletBalnace { get; set; }
    }
}
