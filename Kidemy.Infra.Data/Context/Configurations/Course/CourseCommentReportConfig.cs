using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseCommentReportConfig : IEntityTypeConfiguration<CourseCommentReport>
    {
        public void Configure(EntityTypeBuilder<CourseCommentReport> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasOne(n => n.Comment)
                .WithMany(n => n.CommentReports)
                .HasForeignKey(n => n.CommentId);
        }
    }
}
