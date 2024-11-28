using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Order;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Order
{
    public class OrderRepository : EfRepository<Domain.Models.Order.Order, int>, IOrderRepository
    {
        #region Constructor
        public OrderRepository(KidemyContext context) : base(context)
        {

        }
        #endregion

        public Task<Domain.Models.Order.Order?> GetOrderWithRelations(int orderId)
        {
            return _dbSet.Include(order => order.DiscountUsage)
                         .Include(order => order.Items)
                         .ThenInclude(item => item.DiscountUsage)
                         .FirstOrDefaultAsync(order => order.Id == orderId);
        }
    }
}
