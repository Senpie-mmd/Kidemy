using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Models.Email
{
    public class EmailSetting : AuditBaseEntity<int>
    {
        [Display(Name = "SendFrom")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string From { get; set; }

        [Display(Name = "ShowTitle")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string DisplayName { get; set; }

        [Display(Name = "SMTP")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Smtp { get; set; }

        [Display(Name = "PortNumber")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int Port { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Password { get; set; }

        [Display(Name = "EnableSSL")]
        public bool EnableSsL { get; set; }
    }
}
