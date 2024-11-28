using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kidemy.Domain.Models.Cart
{
    public class CartItem : AuditBaseEntity<int>
    {
        public int CartId { get; set; }

        public int CourseId { get; set; }

        public Cart Cart { get; set; }
    }
}
