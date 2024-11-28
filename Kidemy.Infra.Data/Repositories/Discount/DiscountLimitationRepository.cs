using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Discount;
using Kidemy.Domain.Models.Discount;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Discount
{
    public class DiscountLimitationRepository : EfRepository<DiscountLimitation, int>, IDiscountLimitationRepository
    {
        public DiscountLimitationRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            if (id <= 0) return false;

            var result = await _dbSet
                                    .Where(x => x.Id == id)
                                    .ExecuteUpdateAsync(s => s
                                        .SetProperty(x => x.IsDeleted, true)
                                        .SetProperty(x => x.UpdatedDateOnUtc, DateTime.UtcNow));

            return result > 0;
        }
    }
}
