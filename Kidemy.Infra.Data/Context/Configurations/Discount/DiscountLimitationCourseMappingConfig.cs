using Kidemy.Domain.Models.Discount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Discount
{
    public class DiscountLimitationCourseMappingConfig : IEntityTypeConfiguration<DiscountLimitationCourseMapping>
    {
        public void Configure(EntityTypeBuilder<DiscountLimitationCourseMapping> builder)
        {
            builder.HasKey(e => new {e.DiscountLimitationId, e.CourseId });

            builder.HasOne(e => e.DiscountLimitation)
                .WithMany(e => e.Courses)
                .HasForeignKey(e => e.DiscountLimitationId);
        }
    }    
}
