using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.WithrawRequest
{
    public class WithrawRequestConfig : IEntityTypeConfiguration<Kidemy.Domain.Models.WithdrawRequest.WithdrawRequest>
    {
        public void Configure(EntityTypeBuilder<Kidemy.Domain.Models.WithdrawRequest.WithdrawRequest> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.Property(a => a.RefId).HasMaxLength(100);
        }

    }
}
