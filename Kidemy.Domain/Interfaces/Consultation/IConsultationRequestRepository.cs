using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Consultation;

namespace Kidemy.Domain.Interfaces.Consultation
{
    public interface IConsultationRequestRepository:IRepository<ConsultationRequest,int>
    {
        Task<ConsultationRequest> GetConsultationRequestWithDateAndTypeAsync(int id);

    }
}
