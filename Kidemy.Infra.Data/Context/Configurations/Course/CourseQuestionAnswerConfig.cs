using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseQuestionAnswerConfig : IEntityTypeConfiguration<CourseQuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<CourseQuestionAnswer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
