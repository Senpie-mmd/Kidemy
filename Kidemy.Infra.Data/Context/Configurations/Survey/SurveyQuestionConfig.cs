using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Survey
{
    public class SurveyQuestionConfig : IEntityTypeConfiguration<Domain.Models.Survey.SurveyQuestion>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Survey.SurveyQuestion> builder)
        {
            builder.HasKey(sq => sq.Id);

            builder.HasQueryFilter(sq => !sq.IsDeleted);

            builder.Property(sq => sq.Title).HasMaxLength(500).IsRequired();

            builder.HasOne(sq => sq.Survey)
                   .WithMany(sq => sq.Questions)
                   .HasForeignKey(sq => sq.SurveyId);
        }
    }
}
