using Kidemy.Domain.Models.Discount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Discount
{
    public class DiscountLimitationUsageCountMappingConfig : IEntityTypeConfiguration<DiscountLimitationUsageCountMapping>
    {
        public void Configure(EntityTypeBuilder<DiscountLimitationUsageCountMapping> builder)
        {
            builder.HasKey(e => e.DiscountLimitationId);

            builder.Property(e => e.DiscountLimitationId).ValueGeneratedNever();

            builder.HasOne(e => e.DiscountLimitation)
                .WithOne(e => e.UsageCount)
                .HasForeignKey<DiscountLimitationUsageCountMapping>(e => e.DiscountLimitationId);
        }
    }

    
}
