using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.BankAccountCard
{
    public class BankAccountCardConfig : IEntityTypeConfiguration< Kidemy.Domain.Models.BankAccountCard.BankAccountCard>
    {
        public void Configure(EntityTypeBuilder<Kidemy.Domain.Models.BankAccountCard.BankAccountCard> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.Property(a => a.CardNumber).HasMaxLength(16);
            builder.Property(a => a.ShabaNumber).HasMaxLength(24);
            builder.Property(a => a.AccountNumber).HasMaxLength(50);
        }
    }
}
