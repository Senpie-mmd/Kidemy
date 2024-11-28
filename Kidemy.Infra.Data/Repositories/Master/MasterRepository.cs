using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.DTOs.Master;
using Kidemy.Domain.DTOs.User;
using Kidemy.Domain.Interfaces.Master;
using Kidemy.Domain.Shared;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kidemy.Infra.Data.Repositories.Master
{
    public class MasterRepository : EfRepository<Domain.Models.Master.Master, int>, IMasterRepository
    {
        public MasterRepository(KidemyContext context) : base(context)
        {

        }

        public Task<List<ClientSideBioForMasterModel>> GetMasterBioByUserId(List<int> id)
        {
            IQueryable<Domain.Models.Master.Master> query = _dbSet
            .AsNoTracking()
            .AsQueryable();
            query.Include(x => x.User);
            query = query.Where(n => id.Contains(n.Id));

            return query.Select(master => new ClientSideBioForMasterModel
            {
                Bio = master.Bio,
                UserId = master.User.Id
            }).ToListAsync();
        }
        public async Task<List<int>> GetMastersIds()
        {
            return await _dbSet.Select(n => n.Id).ToListAsync();
        }

        public async Task<bool> MasterIsConfirmed(int id)
        {
            return await _dbSet.AnyAsync(n => n.Id == id && n.Status == Domain.Enums.Master.MasterStatus.Confirmed);
        }

        public async Task<Result<int>> GetCountAsync(Expression<Func<Domain.Models.Master.Master, bool>>? expression = null)
            => expression == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(expression);
    }
}
