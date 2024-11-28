using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.BankAccount;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.BankAccountCard
{
    public class FilterBankAccountCardViewModel : BasePaging<BankAccountCardViewModel>
    {
        [Display(Name = "UserName")]
        public string? UserName { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "CardNumber")]
        [MaxLength(16, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? CardNumber { get; set; }

        [Display(Name = "ShabaNumber")]
        [MaxLength(24, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? ShabaNumber { get; set; }

        [Display(Name = "AccountNumber")]
        [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? AccountNumber { get; set; }

        [Display(Name = "BankAccountCardStatus")]
        public BankAccountCardStatus? Status { get; set; }

        [Display(Name = "IsDefault")]
        public bool? IsDefault { get; set; }
    }
}
