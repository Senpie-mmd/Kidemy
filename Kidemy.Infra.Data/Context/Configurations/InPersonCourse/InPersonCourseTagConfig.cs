using Kidemy.Domain.Models.InPersonCourse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.InPersonCourse
{
    public class InPersonCourseTagConfig : IEntityTypeConfiguration<InPersonCourseTag>
    {
        public void Configure(EntityTypeBuilder<InPersonCourseTag> builder)
        {
            builder.HasKey(e => e.Id );

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(e => e.Title).HasMaxLength(300);
        }
    }
}
