using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kidemy.Domaion.Models.Email
{
    public class SentEmail : BaseEntity<long>
    {
        #region Properties

        [Display(Name = "Email")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Email { get; set; }

        public long SendEmailNotificationId { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public DateTime CreateDate { get; set; }

        #endregion

        #region Relation

        [ForeignKey(nameof(SendEmailNotificationId))]
        public SendEmailNotification SendEmailNotification { get; set; }

        #endregion
    }
}
