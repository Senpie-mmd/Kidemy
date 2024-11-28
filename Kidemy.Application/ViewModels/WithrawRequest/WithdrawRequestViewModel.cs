using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Models.WithdrawRequest;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.WithrawRequest
{
    public class WithdrawRequestViewModel : BaseEntityViewModel<int>
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DataType(DataType.Currency, ErrorMessage = ErrorMessages.FormatError)]
        public decimal? Amount { get; set; }

        public int WalletTransactionId { get; set; }

        [Display(Name = "DestinationBankAccountCard")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int? DestinationBankAccountCardId { get; set; }

        public WithdrawRequestStatus Status { get; set; }

    }
}
