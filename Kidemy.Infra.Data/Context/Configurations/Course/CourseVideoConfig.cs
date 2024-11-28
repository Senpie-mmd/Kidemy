using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseVideoConfig : IEntityTypeConfiguration<CourseVideo>
    {
        public void Configure(EntityTypeBuilder<CourseVideo> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title)
                .HasMaxLength(100);

            builder.Property(n => n.VideoLength)
                .HasMaxLength(10);

            builder.Property(n => n.ThumbnailImageName)
                .HasMaxLength(50);

            builder.Property(n => n.VideoFileName)
                .HasMaxLength(50);

            builder.HasOne(n => n.VideoCategory)
                .WithMany(n => n.Videos)
                .HasForeignKey(n => n.VideoCategoryId);

            builder.HasOne(n => n.Course)
                .WithMany(n => n.Videos)
                .HasForeignKey(n => n.CourseId);

            builder.HasQueryFilter(n => !n.IsDeleted);
        }
    }
}
