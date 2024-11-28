using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseTagMappingConfig : IEntityTypeConfiguration<CourseTagMapping>
    {

        public void Configure(EntityTypeBuilder<CourseTagMapping> builder)
        {
            builder.HasKey(n => new { n.CourseId, n.TagId });

            builder.HasOne(n => n.Course)
                .WithMany(n => n.CourseTags)
                .HasForeignKey(n => n.CourseId);

            builder.HasOne(n => n.Tag)
                .WithMany(n => n.Courses)
                .HasForeignKey(n => n.TagId);
        }
    }
}
