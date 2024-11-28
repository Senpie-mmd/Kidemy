using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Banner
{
    public class BannerConfig : IEntityTypeConfiguration<Domain.Models.Banner.Banner>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Banner.Banner> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(u => u.ImageName).HasMaxLength(100);

            builder.Property(u => u.URL).HasMaxLength(250);

        }
    }
}
