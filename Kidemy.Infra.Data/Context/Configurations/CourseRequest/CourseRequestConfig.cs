using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.CourseRequest
{
    public class CourseRequestConfig : IEntityTypeConfiguration<Domain.Models.CourseRequest.CourseRequest>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.CourseRequest.CourseRequest> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.Property(n => n.Title).HasMaxLength(200);

        }
    }
}
