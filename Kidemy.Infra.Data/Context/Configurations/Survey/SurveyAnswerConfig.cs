using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Survey
{
    public class SurveyAnswerConfig : IEntityTypeConfiguration<Domain.Models.Survey.SurveyAnswer>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Survey.SurveyAnswer> builder)
        {
            builder.HasKey(sw => sw.Id);

            builder.HasQueryFilter(sw => !sw.IsDeleted);

            builder.Property(sw => sw.AnswerText).HasMaxLength(900).IsRequired();

            builder.HasOne(sw => sw.Question)
                   .WithMany(sw => sw.Answers)
                   .HasForeignKey(sw => sw.QuestionId);
        }
    }
}
