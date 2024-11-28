using Kidemy.Domain.Models.Consultation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Consultation
{
    public class AdviserAvailableDateConfig : IEntityTypeConfiguration<Domain.Models.Consultation.AdviserAvailableDate>
    {
        public void Configure(EntityTypeBuilder<AdviserAvailableDate> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Adviser)
                .WithMany(a => a.AvailableDates)
                .HasForeignKey(a => a.AdviserId);

        }
    }
}
