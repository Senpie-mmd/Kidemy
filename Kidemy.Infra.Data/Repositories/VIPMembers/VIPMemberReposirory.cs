using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.VIPMembers;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.VIPMembers
{
    public class VIPMemberReposirory : EfRepository<Domain.Models.VIPMembers.VIPMember, int>, IVIPMemberReposirory
    {
        #region Constructor
        public VIPMemberReposirory(KidemyContext context) : base(context)
        {

        }
        #endregion

        public Task<DateTime> GetUserLastMembershipEndDateAsync(int userId)
        {
            return _dbSet.Where(v => v.UserId == userId).Select(u => u.MembershipEndDate).LastOrDefaultAsync();
        }
    }
}
