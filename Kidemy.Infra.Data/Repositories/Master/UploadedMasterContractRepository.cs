using Barnamenevisan.Localizing.Entity;
using Barnamenevisan.Localizing.Extensions;
using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Repository;
using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Master;
using Kidemy.Domain.Interfaces.Master;
using Kidemy.Domain.Models.Master;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Kidemy.Infra.Data.Repositories.Master
{
    public class UploadedMasterContractRepository : EfRepository<Domain.Models.Master.UploadedMasterContract, int>, IUploadedMasterContractRepository
    {
        public UploadedMasterContractRepository(KidemyContext context) : base(context)
        {

        }
        public async Task<int> GetUploadedMasterContractIdByMasterContractId(int masterContractId)
        {
            return await _dbSet.Where(v => v.MasterContractId == masterContractId).Select(u => u.Id).FirstOrDefaultAsync();
        }

        public Task<List<UploadedMasterContract>> GetUploadedMasterContractsListByMasterId(int masterId)
        {
            return _dbSet.Where(s => s.MasterId == masterId).ToListAsync();
        }

        public async Task FilterMasterContractsByDistinctAsync<TModel>(BasePaging<TModel> filterModel, FilterConditions<UploadedMasterContract> filterConditions, Expression<Func<UploadedMasterContract, TModel>> mapping)
        {
            //IQueryable<UploadedMasterContract> query = _dbSet.Where(e => e.Status == UploadedMasterContractStatus.PendingReview
            //    && e.Id == _dbSet.Where(e2 => e2.MasterId == e.MasterId).Select(e2 => e2.Id).Max());

            IQueryable<UploadedMasterContract> query = _dbSet.FromSqlRaw($@"
                    WITH RankedData AS (
                            SELECT *, ROW_NUMBER() OVER (PARTITION BY MasterId ORDER BY CreatedDateOnUtc DESC) AS RowNum
                            FROM UploadedMasterContract
                            WHERE UploadedMasterContract.Status = 1 AND IsDeleted = 0
                    )
                    SELECT * FROM RankedData
                    WHERE RowNum = 1
            ").IgnoreQueryFilters().AsNoTracking();

            //foreach (Expression<Func<UploadedMasterContract, bool>> filter in filterConditions)
            //{
            //    query = query.Where(filter);
            //}

            await filterModel.Paging(query.ToList().AsQueryable().Select(mapping));
        }
    }
}
