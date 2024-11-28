using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Discount;
using Kidemy.Domain.Models.Discount;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Discount
{
    public class DiscountUsageRepository : EfRepository<DiscountUsage, int>, IDiscountUsageRepository
    {
        public DiscountUsageRepository(KidemyContext context) : base(context)
        {
        }
    }
}
