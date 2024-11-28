using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseCommentConfig : IEntityTypeConfiguration<CourseComment>
    {
        public void Configure(EntityTypeBuilder<CourseComment> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Score)
                .HasMaxLength(15);

            builder.Property(n => n.Message)
                .HasMaxLength(500);

            builder.HasOne(n => n.ReplyComment)
                .WithMany(n => n.RepliedComments)
                .HasForeignKey(n => n.ReplyCommnetId);

            builder.HasOne(n => n.Course)
                .WithMany(n => n.Comments)
                .HasForeignKey(n => n.CourseId);
        }
    }
}
