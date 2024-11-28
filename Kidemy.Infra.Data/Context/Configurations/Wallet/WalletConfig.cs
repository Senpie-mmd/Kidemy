using Kidemy.Domain.Models.Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Wallet
{
    public class WalletConfig : IEntityTypeConfiguration<Domain.Models.Wallet.WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(w => w.IP)
                .HasMaxLength(50);

            builder.Property(w => w.Description)
                .HasMaxLength(700);
            builder.HasIndex(w => w.UserId).IsClustered(false);

        }
    }
}
