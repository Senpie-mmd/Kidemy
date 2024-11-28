using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.AboutUs
{
    public class AboutUs : IEntityTypeConfiguration<Domain.Models.AboutUs.AboutUs>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.AboutUs.AboutUs> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasQueryFilter(a => !a.IsDeleted);

            builder.Property(a => a.Title).HasMaxLength(500);

            builder.Property(a => a.Description).HasMaxLength(4000);

            builder.Property(a => a.OurGoal).HasMaxLength(1000);

            builder.Property(a => a.OurGoalTitle).HasMaxLength(1000);

            builder.Property(a => a.OurGoalDescription).HasMaxLength(5000);
        }
    }
}
