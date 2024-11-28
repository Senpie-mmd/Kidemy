using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Order
{
    public class OrderItemConfig : IEntityTypeConfiguration<Domain.Models.Order.OrderItem>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Order.OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.HasQueryFilter(oi => !oi.IsDeleted);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}
