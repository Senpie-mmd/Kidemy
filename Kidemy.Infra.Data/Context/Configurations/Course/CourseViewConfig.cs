using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseViewConfig : IEntityTypeConfiguration<CourseView>
    {
        public void Configure(EntityTypeBuilder<CourseView> builder)
        {

            builder.HasKey(n => n.Id);

            builder.HasOne(n => n.Course)
                .WithMany(n => n.CourseViews)
                .HasForeignKey(n => n.CourseId);
        }
    }
}
