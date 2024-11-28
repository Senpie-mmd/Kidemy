using Kidemy.Domain.Models.ContactUs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.ContactUs
{
    public class ContactUsFormConfig : IEntityTypeConfiguration<Kidemy.Domain.Models.ContactUs.ContactUsForm>
    {
        public void Configure(EntityTypeBuilder<ContactUsForm> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(100);
        }
    }
}
