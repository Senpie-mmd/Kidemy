using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseNotificationConfig : IEntityTypeConfiguration<Domain.Models.Course.CourseNotification>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Course.CourseNotification> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}
