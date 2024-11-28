using Kidemy.Domain.Models.Discount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Discount
{
    public class DIscountUsageConfig : IEntityTypeConfiguration<DiscountUsage>
    {
        public void Configure(EntityTypeBuilder<DiscountUsage> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.HasOne(e => e.Discount)
                   .WithMany(d => d.DiscountUsages)
                   .HasForeignKey(e => e.DiscountId);

            builder.HasOne(e => e.Order)
                   .WithMany(u => u.DiscountUsage)
                   .HasForeignKey(e => e.OrderId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.OrderItem)
                   .WithOne(u => u.DiscountUsage)
                   .HasForeignKey<DiscountUsage>(e => e.OrderItemId);
        }
    }
}
