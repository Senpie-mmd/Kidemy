using Kidemy.Domain.Models.Discount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Discount
{
    public class DiscountLimitationConfig : IEntityTypeConfiguration<DiscountLimitation>
    {
        public void Configure(EntityTypeBuilder<DiscountLimitation> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.HasOne(e => e.Discount)
                .WithMany(e => e.DiscountLimitations)
                .HasForeignKey(e => e.DiscountId);
        }
    }

    
}
