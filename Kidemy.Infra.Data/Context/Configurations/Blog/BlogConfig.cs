using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Blog
{
    public class BlogConfig : IEntityTypeConfiguration<Domain.Models.Blog.Blog>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Blog.Blog> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasQueryFilter(a => !a.IsDeleted);

            builder.Property(a => a.Title).HasMaxLength(500);

            builder.Property(a => a.ImageName).IsRequired();

            builder.HasMany(a => a.Comments)
                .WithOne(a => a.Blog)
                .HasForeignKey(a => a.BlogId);
        }
    }
}
