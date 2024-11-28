using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Account;
using Kidemy.Domain.Models.Account;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Account
{
    public class AccountCodeRepository : EfRepository<AccountCode, int>, IAccountCodeRepository
    {
        public AccountCodeRepository(KidemyContext context) : base(context)
        {
        }
    }
}
