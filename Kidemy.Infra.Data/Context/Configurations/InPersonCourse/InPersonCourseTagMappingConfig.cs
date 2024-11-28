using Kidemy.Domain.Models.InPersonCourse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.InPersonCourse
{
    public class InPersonCourseTagMappingConfig : IEntityTypeConfiguration<InPersonCourseTagMapping>
    {
        public void Configure(EntityTypeBuilder<InPersonCourseTagMapping> builder)
        {
            builder.HasKey(e => new { e.CourseId, e.TagId });

            builder.HasOne(e => e.Course)
                   .WithMany(e => e.Tags)
                   .HasForeignKey(e => e.CourseId);

            builder.HasOne(e => e.Tag)
                   .WithMany(e => e.Courses)
                   .HasForeignKey(e => e.TagId);
        }
    }
}
