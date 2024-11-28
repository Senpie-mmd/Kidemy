using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domaion.Models.Email
{
    public class SendEmailNotification : BaseEntity<long>
    {
        #region Properties

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int SenderId { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Subject { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Description { get; set; }


        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public DateTime CreateDate { get; set; }


        #endregion

        #region Relations

        public List<SentEmail> SentEmails { get; set; }

        #endregion
    }
}
