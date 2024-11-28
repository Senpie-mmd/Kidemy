using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Cart;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Cart
{
    public class CartRepository : EfRepository<Domain.Models.Cart.Cart, int>, ICartRepository
    {
        public CartRepository(DbContext context) : base(context)
        {
        }

        public async Task ClearUserCartItems(int userId)
        {
            var cart = await _dbSet
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart is null) return;

            cart.Items.Clear();

            _dbSet.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}
