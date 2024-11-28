using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.BankAccount;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.BankAccountCard
{
    public class UpsertBankAccountCardViewModel : BaseEntityViewModel<int>
    {
        public int UserId { get; set; }
        [Display(Name = "OwnerName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string OwnerName { get; set; }

        public string? UserName { get; set; }

        [Display(Name = "BankName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string BankName { get; set; }

        [Display(Name = "CardNumber")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(16, ErrorMessage = ErrorMessages.MaxLengthError)]
        [RegularExpression("^[0-9]+$", ErrorMessage = ErrorMessages.FormatError)]
        public string? CardNumber { get; set; }

        [Display(Name = "ShabaNumber")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(24, ErrorMessage = ErrorMessages.MaxLengthError)]
        [RegularExpression("^[0-9]+$", ErrorMessage = ErrorMessages.FormatError)]
        public string? ShabaNumber { get; set; }

        [Display(Name = "AccountNumber")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
        [RegularExpression("^[0-9]+$", ErrorMessage = ErrorMessages.FormatError)]
        public string? AccountNumber { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "BankAccountCardStatus")]
        public BankAccountCardStatus Status { get; set; }

        public bool IsDefault { get; set; }


    }
}
