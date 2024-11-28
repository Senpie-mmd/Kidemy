using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Master
{
    public class MasterConfig : IEntityTypeConfiguration<Domain.Models.Master.Master>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Master.Master> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.User)
                .WithOne()
                .HasForeignKey<Domain.Models.Master.Master>(m => m.Id);

            builder.HasQueryFilter(m => !m.IsDeleted);

            builder.Property(m => m.Id).ValueGeneratedNever();

            builder.Property(m => m.IdentificationFileName).HasMaxLength(60);

            builder.Property(m => m.FatherName).HasMaxLength(100);

            builder.Property(m => m.NationalIDNumber).HasMaxLength(10);

            builder.Property(m => m.IdentificationNumber).HasMaxLength(10);

            builder.Property(m => m.PlaceOfIssuance).HasMaxLength(100);

            builder.Property(m => m.PostalCode).HasMaxLength(10);
        }
    }
}
