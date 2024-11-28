namespace Kidemy.Domain.Models.Discount
{
    public class DiscountLimitationCategoryMapping
    {
        #region Properties

        public int DiscountLimitationId { get; set; }

        public int CategoryId { get; set; }

        #endregion

        #region Relations

        public DiscountLimitation DiscountLimitation { get; set; }

        #endregion
    }
}
