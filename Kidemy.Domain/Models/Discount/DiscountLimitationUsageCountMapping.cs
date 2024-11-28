
namespace Kidemy.Domain.Models.Discount
{
    public class DiscountLimitationUsageCountMapping
    {
        #region Properties

        public int DiscountLimitationId { get; set; }

        public int Count { get; set; }

        #endregion

        #region Relations

        public DiscountLimitation DiscountLimitation { get; set; }

        #endregion
    }
}
