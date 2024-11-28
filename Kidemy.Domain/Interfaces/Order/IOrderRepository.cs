using Barnamenevisan.Localizing.Repository;

namespace Kidemy.Domain.Interfaces.Order
{
    public interface IOrderRepository : IRepository<Models.Order.Order, int>
    {
        Task<Models.Order.Order?> GetOrderWithRelations(int orderId);
    }
}
