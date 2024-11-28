using Kidemy.Domain.Statics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.User
{
    public class UserConfig : IEntityTypeConfiguration<Domain.Models.User.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.User.User> builder)
        {
            builder.HasKey(u=>u.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(u=>u.Email)
                .HasMaxLength(350);
            
            builder.Property(u=>u.Mobile).HasMaxLength(15);
            
            builder.Property(u=>u.FirstName).HasMaxLength(250);
            
            builder.Property(u=>u.LastName).HasMaxLength(250);
            
            builder.Property(u=>u.Password).HasMaxLength(300);
            
            builder.Property(u=>u.AvatarName).HasMaxLength(50);
        }
    }
}
