using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Consultation;
using Kidemy.Domain.Models.Consultation;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Consultation
{
    public class AdviserAvailableDateRepository : EfRepository<AdviserAvailableDate, int>, IAdviserAvailableDateRepository
    {
        public AdviserAvailableDateRepository(DbContext context) : base(context)
        {
        }
    }
}
