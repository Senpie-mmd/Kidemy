using Kidemy.Domain.Models.InPersonCourse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.InPersonCourse
{
    public class InPersonCourseVisitConfig : IEntityTypeConfiguration<InPersonCourseVisit>
    {
        public void Configure(EntityTypeBuilder<InPersonCourseVisit> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
