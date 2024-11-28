using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.VIPMembers;
using Kidemy.Domain.Shared;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.VIPMembers
{
    public class VIPPlanRepository : EfRepository<Domain.Models.VIPMembers.VIPPlan, int>, IVIPPlanRepository
    {
        #region Constructor
        public VIPPlanRepository(KidemyContext context) : base(context)
        {

        }
        #endregion

        public Task<decimal> GetAmountById(int id)
        {
            return _dbSet.Where(p=>p.Id==id).Select(p=>p.Price).FirstOrDefaultAsync();
        }

        public Task<int> GetDurationDayById(int id)
        {
            return _dbSet.Where(p => p.Id == id).Select(p => p.DurationDay).FirstOrDefaultAsync();
        }
    }
}
