using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.Master
{
    public class MasterContractConfig : IEntityTypeConfiguration<Domain.Models.Master.MasterContract>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Master.MasterContract> builder)
        {
            builder.HasKey(mc => mc.Id);

            builder.Property(mc => mc.FileName).HasMaxLength(60);

            builder.Property(mc => mc.Title).HasMaxLength(100);

            builder.HasQueryFilter(mc => !mc.IsDeleted);
        }
    }
}
