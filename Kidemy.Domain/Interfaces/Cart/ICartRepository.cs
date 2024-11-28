using Barnamenevisan.Localizing.Repository;

namespace Kidemy.Domain.Interfaces.Cart
{
    public interface ICartRepository : IRepository<Domain.Models.Cart.Cart, int>
    {
        Task ClearUserCartItems(int userId);
    }
}
