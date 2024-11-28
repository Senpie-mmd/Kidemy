using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.InPersonCourse
{
    public class InPersonCourseConfig : IEntityTypeConfiguration<Domain.Models.InPersonCourse.InPersonCourse>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.InPersonCourse.InPersonCourse> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(e => e.Title).HasMaxLength(300);

            builder.Property(e => e.ShortDescription).HasMaxLength(750);

            builder.Property(e => e.ImageName).HasMaxLength(60);

            builder.Property(e => e.AttachmentFileName).HasMaxLength(60);
        }
    }
}
