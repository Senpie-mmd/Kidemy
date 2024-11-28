using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.SocialMedia
{
    public class SocialMediaConfig : IEntityTypeConfiguration<Domain.Models.SocialMedia.SocialMedia>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.SocialMedia.SocialMedia> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasQueryFilter(s => !s.IsDeleted);

            builder.Property(s => s.Link).HasMaxLength(300);

            builder.Property(s => s.Title).HasMaxLength(200);

            builder.Property(s => s.ImageName).HasMaxLength(100);
        }
    }
}