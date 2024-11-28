using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Survey
{
    public class SurveyConfig : IEntityTypeConfiguration<Domain.Models.Survey.Survey>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Survey.Survey> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasQueryFilter(s => !s.IsDeleted);

            builder.Property(s => s.Title).HasMaxLength(500).IsRequired();
        }
    }
}
