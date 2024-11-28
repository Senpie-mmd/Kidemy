using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Wallet;
using Kidemy.Domain.Models.SettlementTransaction;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories
{
    public class SettlementTransactionRepository : EfRepository<SettlementTransaction, int>, ISettlementTransactionRepository
    {
        public SettlementTransactionRepository(DbContext context) : base(context)
        {
        }
    }
}
