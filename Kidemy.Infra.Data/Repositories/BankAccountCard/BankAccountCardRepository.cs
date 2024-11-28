using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.BankAccountCard;
using Microsoft.EntityFrameworkCore;

namespace GoftemanMelal.Infra.Data.Repositories.BankAccountCard
{
    public class BankAccountCardRepository : EfRepository<Kidemy.Domain.Models.BankAccountCard.BankAccountCard, int>, IBankAccountCardRepository
    {
        public BankAccountCardRepository(DbContext context) : base(context)
        {
        }

    }
}
