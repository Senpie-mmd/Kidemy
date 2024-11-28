using Kidemy.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.User
{
    public class UserRoleMappingConfig : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {

            builder.HasKey(u => new { u.RoleId, u.UserId });

            builder
                .HasOne(u => u.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(u => u.UserId);
        }
    }
}
