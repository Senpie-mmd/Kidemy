using Barnamenevisan.Localizing.Repository;
using System.Linq.Expressions;

namespace Kidemy.Domain.Interfaces.WithdrawRequest
{
    public interface IWithdrawRequestRepository : IRepository<Models.WithdrawRequest.WithdrawRequest, int>
    {
        Task<int> GetCountAsync(Expression<Func<Models.WithdrawRequest.WithdrawRequest, bool>>? expression = null);

    }
}
