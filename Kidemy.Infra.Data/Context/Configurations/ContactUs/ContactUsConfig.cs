using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.ContactUs
{
    public class ContactUsConfig : IEntityTypeConfiguration<Kidemy.Domain.Models.ContactUs.ContactUs>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.ContactUs.ContactUs> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Address).HasMaxLength(500);

            builder.Property(x => x.Mobile).HasMaxLength(15);

            builder.Property(x => x.Email).HasMaxLength(100);

        }
    }
}
