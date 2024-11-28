using Kidemy.Domain.Models.Consultation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Consultation
{
    public class AdviserConsultationTypeConfig : IEntityTypeConfiguration<Domain.Models.Consultation.AdviserConsultationType>
    {
        public void Configure(EntityTypeBuilder<AdviserConsultationType> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Adviser)
                .WithMany(a => a.ConsultationTypes)
                .HasForeignKey(a => a.AdviserId);

        }
    }
}
