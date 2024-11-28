using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Payment;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Models.SiteSetting
{
    public class SiteSetting : BaseEntity<int>
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailFormatError)]
        [MaxLength(350, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Email { get; set; }

        [Display(Name = "Mobile")]
        [MaxLength(15, ErrorMessage = ErrorMessages.MaxLengthError)]
        [RegularExpression("^[0-9]*$", ErrorMessage = ErrorMessages.MobileFormatError)]
        public string? Mobile { get; set; }

        [Display(Name = "Mobile")]
        [MaxLength(15, ErrorMessage = ErrorMessages.MaxLengthError)]
        [RegularExpression("^[0-9]*$", ErrorMessage = ErrorMessages.MobileFormatError)]
        public string? Mobile2 { get; set; }

        [Display(Name = "Address")]
        [MaxLength(800, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Address { get; set; }

        [Display(Name = "Logo")]
        [MaxLength(50)]
        public string? LogoName { get; set; }

        [Display(Name = "CollectionManagement")]
        public string? CollectionManagement { get; set; }

        [Display(Name = "FooterDescription")]
        public string? FooterDescription { get; set; }

        [Display(Name = "CopyrightDescription")]
        public string? CopyrightDescription { get; set; }

        public PaymentMethodType DefaultPaymentMethod { get; set; }

        [Display(Name = "NewsletterDescription")]
        public string? NewsletterDescription { get; set; }

        public decimal WithdrawRequestMinimumAmount { get; set; }

    }
}
