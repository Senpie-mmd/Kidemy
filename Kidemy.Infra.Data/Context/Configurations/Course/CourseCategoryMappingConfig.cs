using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseCategoryMappingConfig : IEntityTypeConfiguration<CourseCategoryMapping>
    {
        public void Configure(EntityTypeBuilder<CourseCategoryMapping> builder)
        {
            builder.HasKey(n => new { n.CourseId, n.CategoryId });

            builder.HasOne(n => n.Course)
                .WithMany(n => n.Categories)
                .HasForeignKey(n => n.CourseId);

            builder.HasOne(n => n.Category)
                .WithMany(n => n.Courses)
                .HasForeignKey(n => n.CategoryId);
        }
    }
}
