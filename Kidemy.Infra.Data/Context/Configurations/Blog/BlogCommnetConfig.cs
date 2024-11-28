using Kidemy.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Kidemy.Infra.Data.Context.Configurations.Blog
{
    public class BlogCommnetConfig : IEntityTypeConfiguration<BlogComment>
    {
        public void Configure(EntityTypeBuilder<BlogComment> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Message)
            .HasMaxLength(500);

            builder.HasOne(bc => bc.ParentComment)
                   .WithMany(bc => bc.ReplyComments)
                   .HasForeignKey(bc => bc.ReplyCommentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(n => n.Blog)
                .WithMany(n => n.Comments)
                .HasForeignKey(n => n.BlogId);
        }
    }
}
