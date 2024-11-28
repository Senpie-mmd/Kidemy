using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.ContactUs;

namespace Kidemy.Domain.Models.ContactUs
{
    public class ContactUsForm : BaseEntity<int>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        public ContactUsFormState State { get; set; }

        public DateTime CreateDate { get; set; }

        public int? AnswererId { get; set; }

        public int? ParentId { get; set; }
        public int UserId { get; set; }

        #region Relation

        public ContactUsForm? Parent { get; set; }

        #endregion
    }
}
