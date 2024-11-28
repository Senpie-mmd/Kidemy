
namespace Kidemy.Domain.Models.Discount
{
    public class DiscountLimitationUserMapping
    {
        #region Properties
        
        public int DiscountLimitationId { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Relations

        public DiscountLimitation DiscountLimitation { get; set; }

        #endregion
    }
}
