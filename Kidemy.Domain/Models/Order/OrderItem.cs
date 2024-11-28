using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Models.Discount;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kidemy.Domain.Models.Order
{
    public class OrderItem : AuditBaseEntity<int>
    {
        public int OrderId { get; set; }

        public int CourseId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? DiscountedUnitPrice { get; set; }

        public Order Order { get; set; }

        public DiscountUsage DiscountUsage { get; set; }
    }
}
