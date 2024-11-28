using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseUserMappingConfig : IEntityTypeConfiguration<CourseUserMapping>
    {
        public void Configure(EntityTypeBuilder<CourseUserMapping> builder)
        {
            builder.HasKey(n => new { n.CourseId, n.UserId });

            builder.HasOne(e => e.Course)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.CourseId);
        }
    }
}
