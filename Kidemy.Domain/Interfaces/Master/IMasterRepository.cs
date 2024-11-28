using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.DTOs.Master;
using Kidemy.Domain.DTOs.User;
using Kidemy.Domain.Shared;
using System.Linq.Expressions;

namespace Kidemy.Domain.Interfaces.Master
{
    public interface IMasterRepository : IRepository<Domain.Models.Master.Master, int>
    {
        Task<Result<int>> GetCountAsync(Expression<Func<Domain.Models.Master.Master, bool>>? expression = null);
        Task<List<ClientSideBioForMasterModel>> GetMasterBioByUserId(List<int> id);
        Task<List<int>> GetMastersIds();
        Task<bool> MasterIsConfirmed(int id);
    }
}
