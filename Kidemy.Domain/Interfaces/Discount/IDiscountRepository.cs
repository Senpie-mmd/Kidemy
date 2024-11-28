using Barnamenevisan.Localizing.Repository;

namespace Kidemy.Domain.Interfaces.Discount
{
    public interface IDiscountRepository : IRepository<Domain.Models.Discount.Discount, int>
    {
        Task<List<Domain.Models.Discount.Discount>> GetAllActiveDiscounts();
        Task<bool> SoftDeleteAsync(int id);
    }
}
