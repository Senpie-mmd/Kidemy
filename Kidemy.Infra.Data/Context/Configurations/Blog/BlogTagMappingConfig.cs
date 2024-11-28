using Kidemy.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Blog
{
    public class BlogTagMappingConfig : IEntityTypeConfiguration<BlogTagMapping>
    {

        public void Configure(EntityTypeBuilder<BlogTagMapping> builder)
        {
            builder.HasKey(n => new { n.BlogId, n.TagId });

            builder.HasOne(n => n.Blog)
                .WithMany(n => n.BlogTags)
                .HasForeignKey(n => n.BlogId);

            builder.HasOne(n => n.Tag)
                .WithMany(n => n.Blogs)
                .HasForeignKey(n => n.TagId);
        }
    }
}
