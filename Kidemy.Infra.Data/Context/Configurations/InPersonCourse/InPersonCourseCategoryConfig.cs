using Kidemy.Domain.Models.InPersonCourse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.InPersonCourse
{
    public class InPersonCourseCategoryMappingConfig : IEntityTypeConfiguration<InPersonCourseCategoryMapping>
    {
        public void Configure(EntityTypeBuilder<InPersonCourseCategoryMapping> builder)
        {
            builder.HasKey(e => new { e.CourseId, e.CategoryId });

            builder.HasOne(e => e.InPersonCourse)
                   .WithMany(e => e.Categories)
                   .HasForeignKey(e => e.CourseId);

            builder.HasOne(e => e.CourseCategory)
                   .WithMany(e => e.InPersonCourses)
                   .HasForeignKey(e => e.CategoryId);
        }
    }
}
