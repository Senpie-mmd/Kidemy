using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Discount;

namespace Kidemy.Domain.Interfaces.Discount
{
    public interface IDiscountLimitationRepository : IRepository<DiscountLimitation, int>
    {
        Task<bool> SoftDeleteAsync(int id);
    }
}
