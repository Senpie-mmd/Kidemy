using Kidemy.Domain.Models.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Cart
{
    public class CartItemConfig : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(item => item.Cart)
                   .WithMany(cart => cart.Items)
                   .HasForeignKey(item => item.CartId);
        }
    }
}
