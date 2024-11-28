using Kidemy.Domain.Models.AboutUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.AboutUs
{
    public class AboutUsProgressBarConfig : IEntityTypeConfiguration<AboutUsProgressBar>
    {
        public void Configure(EntityTypeBuilder<AboutUsProgressBar> builder)
        {
            builder.HasKey(n => n.Id);
        }
    }
}
