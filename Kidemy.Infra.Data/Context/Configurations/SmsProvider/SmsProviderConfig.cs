using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.SmsProvider
{
    public class SmsProviderConfig : IEntityTypeConfiguration<Domain.Models.SmsProvider.SmsProvider>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.SmsProvider.SmsProvider> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.HasQueryFilter(sp => !sp.IsDeleted);

        }
    }
}
