using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Models.Discount;

namespace Kidemy.Domain.Models.Order
{
    public class Order : AuditBaseEntity<int>
    {
        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal? DiscountedTotalAmount { get; set; }

        public bool IsPaid { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public ICollection<DiscountUsage> DiscountUsage { get; set; }
    }
}
