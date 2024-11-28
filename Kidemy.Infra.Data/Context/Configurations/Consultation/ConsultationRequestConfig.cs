using Kidemy.Domain.Models.Consultation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Consultation
{
    public class ConsultationRequestConfig : IEntityTypeConfiguration<Domain.Models.Consultation.ConsultationRequest>
    {
        public void Configure(EntityTypeBuilder<ConsultationRequest> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasQueryFilter(a => !a.IsDeleted);

            builder.HasOne(a => a.Adviser)
                 .WithMany(a => a.ConsultationRequests)
                 .HasForeignKey(a => a.AdviserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.SelectedDate)
               .WithMany(a => a.ConsultationRequest)
               .HasForeignKey(a => a.SelectedDateId);

            builder.HasOne(a => a.AdviserConsultationType)
               .WithMany(a => a.ConsultationRequest)
               .HasForeignKey(a => a.AdviserConsultationTypeId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
