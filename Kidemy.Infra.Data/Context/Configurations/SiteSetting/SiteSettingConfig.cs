using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.SiteSetting
{
    public class SiteSettingConfig : IEntityTypeConfiguration<Domain.Models.SiteSetting.SiteSetting>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.SiteSetting.SiteSetting> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasQueryFilter(s => !s.IsDeleted);

            builder.Property(s => s.Email).HasMaxLength(350);

            builder.Property(s => s.Mobile).HasMaxLength(15);

            builder.Property(s => s.Mobile2).HasMaxLength(15);

            builder.Property(s => s.Address).HasMaxLength(800);

            builder.Property(s => s.LogoName).HasMaxLength(50);
        }
    }
}


