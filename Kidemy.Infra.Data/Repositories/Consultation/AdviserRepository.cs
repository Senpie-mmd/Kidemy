using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Consultation;
using Kidemy.Domain.Models.Consultation;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Consultation
{
    public class AdviserRepository : EfRepository<Adviser, int>, IAdviserRepository
    {
        public AdviserRepository(DbContext context) : base(context)
        {
        }

        public async Task<Adviser> GetAdviserWithDatesAndTypesAsync(int id)
        {
            return await _context.Set<Domain.Models.Consultation.Adviser>()
                .Include(x=>x.AvailableDates)
                .Include(x=>x.ConsultationTypes)
                .Where(x=>!x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
