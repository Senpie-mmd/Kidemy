using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Discount;

namespace Kidemy.Domain.Models.Discount
{
    public class DiscountLimitation : AuditBaseEntity<int>
    {
        #region Properties

        public int DiscountId { get; set; }

        public DiscountLimitationType Type { get; set; }

        #endregion

        #region Relations

        public Discount Discount { get; set; }

        public ICollection<DiscountLimitationUserMapping> Users { get; set; }

        public ICollection<DiscountLimitationCourseMapping> Courses { get; set; }

        public ICollection<DiscountLimitationCategoryMapping> Categories { get; set; }

        public DiscountLimitationUsageCountMapping? UsageCount { get; set; }

        #endregion
    }
}
