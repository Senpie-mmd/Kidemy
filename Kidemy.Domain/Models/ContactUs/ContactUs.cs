using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.ContactUs
{
    public class ContactUs : BaseEntity<int>
    {
        public string Address { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDateOnUtc { get; set; }
        public DateTime UpdatedDateOnUtc { get; set; }
    }
}
