using Kidemy.Domain.Models.Consultation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Consultation
{
    public class AdviserConfig : IEntityTypeConfiguration<Domain.Models.Consultation.Adviser>
    {
        public void Configure(EntityTypeBuilder<Adviser> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}
