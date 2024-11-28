using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Master;
using System.Linq.Expressions;

namespace Kidemy.Domain.Interfaces.Master
{
    public interface IUploadedMasterContractRepository : IRepository<Domain.Models.Master.UploadedMasterContract, int>
    {
        Task FilterMasterContractsByDistinctAsync<TModel>(Barnamenevisan.Localizing.Shared.BasePaging<TModel> filterModel, FilterConditions<UploadedMasterContract> filterConditions, Expression<Func<UploadedMasterContract, TModel>> mapping);
        Task<int> GetUploadedMasterContractIdByMasterContractId(int masterContractId);
        Task<List<UploadedMasterContract>> GetUploadedMasterContractsListByMasterId(int masterId);
    }
}
