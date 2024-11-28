using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.Page
{
    public class PageConfig : IEntityTypeConfiguration<Domain.Models.Page.Page>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Page.Page> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.Property(p => p.Title).HasMaxLength(500);
        }
    }
}
