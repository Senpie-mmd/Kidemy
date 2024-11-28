using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.MasterNotification
{
    public class NotificationMasterMappingConfig : IEntityTypeConfiguration<Domain.Models.MasterNotification.NotificationMasterMapping>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.MasterNotification.NotificationMasterMapping> builder)
        {
            builder.HasKey(u => new { u.MasterNotificationId, u.MasterId });

            builder
                .HasOne(u => u.MasterNotification)
                .WithMany(u => u.Masters)
                .HasForeignKey(u => u.MasterNotificationId);


        }
    }
}
