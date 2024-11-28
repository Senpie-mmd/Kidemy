using Kidemy.Domain.Models.Discount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Discount
{
    public class DiscountLimitationUserMappingConfig : IEntityTypeConfiguration<DiscountLimitationUserMapping>
    {
        public void Configure(EntityTypeBuilder<DiscountLimitationUserMapping> builder)
        {
            builder.HasKey(e => new {e.DiscountLimitationId, e.UserId });

            builder.HasOne(e => e.DiscountLimitation)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.DiscountLimitationId);
        }
    }

    
}
