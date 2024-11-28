using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Course
{
    public class FavourtieCourseConfig : IEntityTypeConfiguration<Domain.Models.Course.FavouriteCourse>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Course.FavouriteCourse> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}
