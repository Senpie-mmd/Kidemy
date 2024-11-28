using Kidemy.Domain.Models.Discount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Discount
{
    public class DiscountLimitationCategoryMappingConfig : IEntityTypeConfiguration<DiscountLimitationCategoryMapping>
    {
        public void Configure(EntityTypeBuilder<DiscountLimitationCategoryMapping> builder)
        {
            builder.HasKey(e => new {e.DiscountLimitationId, e.CategoryId });

            builder.HasOne(e => e.DiscountLimitation)
                .WithMany(e => e.Categories)
                .HasForeignKey(e => e.DiscountLimitationId);
        }
    }

    
}
