using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.WithdrawRequest;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kidemy.Infra.Data.Repositories.WithdrawRequest
{
    public class WithdrawRequestRepository : EfRepository<Domain.Models.WithdrawRequest.WithdrawRequest, int>, IWithdrawRequestRepository
    {
        public WithdrawRequestRepository(DbContext context) : base(context)
        {
        }

        public async Task<int> GetCountAsync(Expression<Func<Domain.Models.WithdrawRequest.WithdrawRequest, bool>>? expression = null)
            => expression == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(expression);
    }
}
