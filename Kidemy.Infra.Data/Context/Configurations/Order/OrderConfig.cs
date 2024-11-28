using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Order
{
    public class OrderConfig : IEntityTypeConfiguration<Domain.Models.Order.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Order.Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasQueryFilter(o => !o.IsDeleted);
        }
    }
}
