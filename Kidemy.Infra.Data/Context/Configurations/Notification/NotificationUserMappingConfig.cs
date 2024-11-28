using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.Notification
{
    public class NotificationUserMappingConfig : IEntityTypeConfiguration<Domain.Models.Notification.NotificationUserMapping>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Notification.NotificationUserMapping> builder)
        {
            builder.HasKey(u => new { u.NotificationId, u.UserId });

            builder
                .HasOne(u => u.Notification)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.NotificationId);


        }
    }
}
