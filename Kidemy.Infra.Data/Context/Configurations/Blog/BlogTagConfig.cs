using Kidemy.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Blog
{
    public class BlogTagConfig : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title).HasMaxLength(100);
        }
    }
}
