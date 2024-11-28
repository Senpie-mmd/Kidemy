using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.MasterNotification
{
    public class MasterNotificationConfig : IEntityTypeConfiguration<Domain.Models.MasterNotification.MasterNotification>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.MasterNotification.MasterNotification> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);


        }
    }
}
