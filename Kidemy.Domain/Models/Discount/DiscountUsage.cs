using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Models.Order;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kidemy.Domain.Models.Discount
{
    public class DiscountUsage : AuditBaseEntity<int>
    {
        public int UsedByUserId { get; set; }

        public int DiscountId { get; set; }

        public int? OrderItemId { get; set; }

        public int OrderId { get; set; }

        public decimal ReducedAmount { get; set; }

        public bool IsFinally { get; set; }

        public Discount Discount { get; set; }

        public OrderItem? OrderItem { get; set; }

        public Order.Order Order { get; set; }
    }
}
