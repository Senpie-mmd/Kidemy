using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseTagConfig : IEntityTypeConfiguration<CourseTag>
    {
        public void Configure(EntityTypeBuilder<CourseTag> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title).HasMaxLength(100);
        }
    }
}
