using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Notification
{
    public class NotificationConfig : IEntityTypeConfiguration<Domain.Models.Notification.Notification>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Notification.Notification> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(u => u.Subject).HasMaxLength(250);


        }
    }
}
