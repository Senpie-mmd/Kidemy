using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.Link
{
    public class LinkConfig : IEntityTypeConfiguration<Domain.Models.Link.Link>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Link.Link> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasQueryFilter(l => !l.IsDeleted);

            builder.Property(l => l.Title).HasMaxLength(50);

            builder.Property(l => l.Address).HasMaxLength(500);

        }
    }
}
