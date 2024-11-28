using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Slider
{
    public class SliderConfig : IEntityTypeConfiguration<Domain.Models.Slider.Slider>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Slider.Slider> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasQueryFilter(s => !s.IsDeleted);

            builder.Property(s => s.Title).HasMaxLength(300);

            builder.Property(s => s.Description).HasMaxLength(900);

            builder.Property(s => s.ImageName).HasMaxLength(100);

            builder.Property(s => s.URL).HasMaxLength(250);

        }
    }
}
