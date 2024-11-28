using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Discount;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideDiscountViewModel : BaseEntityViewModel<int>
    {
        public string? Title { get; set; }

        public string? Code { get; set; }

        public decimal? Value { get; set; }

        public bool IsPercentage { get; set; }

        public DateTime? StartDateTimeOnUtc { get; set; }

        public DateTime? EndDateTimeOnUtc { get; set; }

        public bool IsActive { get; set; }

        public bool AutoUse { get; set; }

        public DiscountType Type { get; set; }
    }
}
