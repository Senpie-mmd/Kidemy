using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Wallet
{
    public class ChargeWalletViewModel 
    {
        public int? UserId { get; set; }

        public string? UserIP { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DataType(DataType.Currency,ErrorMessage =ErrorMessages.FormatError)]
		public decimal? Amount { get; set; }

        public decimal? Balance { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string? Description { get; set; }
        
    }
}
