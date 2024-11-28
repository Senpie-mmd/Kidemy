using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class CourseConfig : IEntityTypeConfiguration<Domain.Models.Course.Course>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Course.Course> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasQueryFilter(n => !n.IsDeleted);
        }
    }
}
