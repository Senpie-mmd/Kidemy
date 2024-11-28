using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseQuestionConfig : IEntityTypeConfiguration<CourseQuestion>
    {
        public void Configure(EntityTypeBuilder<CourseQuestion> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title).HasMaxLength(100);

            builder.HasOne(c => c.Course)
                .WithMany(c => c.CourseQuestions)
                .HasForeignKey(c => c.CourseId);

            builder.HasMany(n => n.Answers)
                .WithOne(n => n.Question)
                .HasForeignKey(n => n.QuestionId);

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
