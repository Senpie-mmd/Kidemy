using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Consultation;
using Kidemy.Domain.Models.Consultation;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Consultation
{
    public class ConsultationRequestRepository : EfRepository<ConsultationRequest, int>, IConsultationRequestRepository
    {
        public ConsultationRequestRepository(DbContext context) : base(context)
        {
        }

        public async Task<ConsultationRequest> GetConsultationRequestWithDateAndTypeAsync(int id)
        {
            return await _context.Set<Domain.Models.Consultation.ConsultationRequest>()
                .Include(x => x.AdviserConsultationType)
                .Include(x => x.SelectedDate)
                .Include(x=>x.Adviser)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }
    }
}
