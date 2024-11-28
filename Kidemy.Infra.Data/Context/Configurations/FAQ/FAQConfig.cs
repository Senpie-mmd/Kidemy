using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.FAQ
{
    public class FAQConfig : IEntityTypeConfiguration<Domain.Models.FAQ.FAQ>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.FAQ.FAQ> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasQueryFilter(f => !f.IsDeleted);

            builder.Property(f => f.Title).HasMaxLength(200);

        }
    }
}
