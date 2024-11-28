using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Discount
{
    public class DiscountConfig : IEntityTypeConfiguration<Domain.Models.Discount.Discount>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Discount.Discount> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(e => e.Title).HasMaxLength(200);

            builder.Property(e => e.Code).HasMaxLength(50);
        }
    }
}
