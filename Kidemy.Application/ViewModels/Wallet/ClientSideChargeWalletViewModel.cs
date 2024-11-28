using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Wallet
{
    public class ClientSideChargeWalletViewModel
    {
        public int? UserId { get; set; }

        public string? UserIP { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DataType(DataType.Currency, ErrorMessage = ErrorMessages.FormatError)]
        [Range(1000, int.MaxValue, ErrorMessage = ErrorMessages.MaxError)]
        public decimal? Amount { get; set; }
    }
}
