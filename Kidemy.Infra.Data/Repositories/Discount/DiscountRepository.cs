using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Discount;
using Kidemy.Domain.Models.Discount;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Discount
{
    public class DiscountRepository : EfRepository<Domain.Models.Discount.Discount, int>, IDiscountRepository
    {
        public DiscountRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var result = await _dbSet
                                    .Where(x => x.Id == id)
                                    .ExecuteUpdateAsync(s => s
                                    .SetProperty(x => x.IsDeleted, true)
                                    .SetProperty(x => x.UpdatedDateOnUtc, DateTime.UtcNow));

            return result > 0;
        }

        public Task<List<Domain.Models.Discount.Discount>> GetAllActiveDiscounts()
        {
            return _dbSet
                        .Where(d => d.IsActive)
                        .Include(d => d.DiscountUsages)
                        .Include(d => d.DiscountLimitations)
                            .ThenInclude(dl => dl.Users)
                        .Include(d => d.DiscountLimitations)
                            .ThenInclude(dl => dl.Categories)
                        .Include(d => d.DiscountLimitations)
                            .ThenInclude(dl => dl.Courses)
                        .Include(d => d.DiscountLimitations)
                            .ThenInclude(dl => dl.UsageCount)
                        .ToListAsync();
        }
    }
}
