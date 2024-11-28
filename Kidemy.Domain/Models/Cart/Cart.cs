using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Cart
{
    public class Cart : AuditBaseEntity<int>
    {
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public int UserId { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}
