using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseCategoryConfig : IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Title).HasMaxLength(100);

            builder.HasMany(n => n.SubCategories)
                .WithOne(n => n.ParentCategory)
                .HasForeignKey(n => n.ParentCourseCategoryId);

            builder.HasQueryFilter(n => !n.IsDeleted);
        }
    }
}
