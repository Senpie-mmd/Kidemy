using Kidemy.Domain.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Identity
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(p => p.UniqueName).IsRequired()
                .HasMaxLength(350);
        }
    }
}
