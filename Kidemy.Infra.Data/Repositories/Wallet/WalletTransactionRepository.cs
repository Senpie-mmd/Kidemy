using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Wallet;
using Kidemy.Domain.Models.Wallet;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Wallet
{
    public class WalletTransactionRepository : EfRepository<WalletTransaction, int>, IWalletTransactionRepository
    {
        public WalletTransactionRepository(KidemyContext context) : base(context)
        {
        }
    }
}
