using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Cart
{
    public class CartConfig : IEntityTypeConfiguration<Domain.Models.Cart.Cart>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Cart.Cart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
