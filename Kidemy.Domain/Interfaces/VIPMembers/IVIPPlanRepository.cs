using Barnamenevisan.Localizing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Interfaces.VIPMembers
{
    public interface IVIPPlanRepository : IRepository<Kidemy.Domain.Models.VIPMembers.VIPPlan, int>
    {
        Task<decimal> GetAmountById(int id);
        Task<int> GetDurationDayById(int id);
    }
}
