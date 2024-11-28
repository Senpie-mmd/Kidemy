using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Discount;

namespace Kidemy.Domain.Models.Discount
{
    public class Discount : AuditBaseEntity<int>
    {
        #region Properties

        public string? Title { get; set; }

        public string? Code { get; set; }

        public decimal Value { get; set; }

        public bool IsPercentage { get; set; }

        public DateTime? StartDateTimeOnUtc { get; set; }

        public DateTime? EndDateTimeOnUtc { get; set; }

        public bool IsActive { get; set; }

        public bool AutoUse { get; set; }

        public DiscountType Type { get; set; }

        #endregion

        #region Relations

        public ICollection<DiscountUsage> DiscountUsages { get; set; }

        public ICollection<DiscountLimitation> DiscountLimitations { get; set; }

        #endregion
    }
}
