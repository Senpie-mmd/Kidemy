using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.Newsletter
{
    public class NewsletterConfig : IEntityTypeConfiguration<Domain.Models.Newsletter.Newsletter>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Newsletter.Newsletter> builder)
        {
            builder.HasKey(nl => nl.Id);

            builder.HasQueryFilter(nl => !nl.IsDeleted);

            builder.Property(nl => nl.Email).HasMaxLength(350);

        }
    }
}
