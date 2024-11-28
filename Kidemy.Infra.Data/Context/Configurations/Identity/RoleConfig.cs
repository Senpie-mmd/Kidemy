using Kidemy.Domain.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Identity
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(r => r.Title).IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.UniqueName).IsRequired()
                .HasMaxLength(200);
        }
    }
}
