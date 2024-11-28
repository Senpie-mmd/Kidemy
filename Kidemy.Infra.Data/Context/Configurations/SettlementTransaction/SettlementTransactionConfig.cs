using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.SettlementTransaction
{
    public class SettlementTransactionConfig : IEntityTypeConfiguration<Domain.Models.SettlementTransaction.SettlementTransaction>
    {
        public void Configure(EntityTypeBuilder< Domain.Models.SettlementTransaction.SettlementTransaction> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.Property(a => a.CardNumber).HasMaxLength(16);
            builder.Property(a => a.AccountNumber).HasMaxLength(50);
        }
    }
}
