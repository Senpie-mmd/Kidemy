using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Consultation;

namespace Kidemy.Domain.Interfaces.Consultation
{
    public interface IAdviserRepository : IRepository<Adviser, int>
    {
        Task<Adviser> GetAdviserWithDatesAndTypesAsync(int id);
    }
}
