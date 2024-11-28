using Kidemy.Domain.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Identity
{
    public class RolePermissionMappingConfig : IEntityTypeConfiguration<RolePermissionMapping>
    {
        public void Configure(EntityTypeBuilder<RolePermissionMapping> builder)
        {

            builder.HasKey(r => new { r.RoleId, r.PermissionId });

            builder
                .HasOne(r => r.Role)
                .WithMany(u => u.Permissions)
                .HasForeignKey(b => b.RoleId);

            builder
                .HasOne(r => r.Permission)
                .WithMany(u => u.Roles)
                .HasForeignKey(u => u.PermissionId);
        }
    }
}
