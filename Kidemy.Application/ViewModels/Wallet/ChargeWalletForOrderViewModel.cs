using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Wallet
{
    public class ChargeWalletForOrderViewModel
    {
        public int? UserId { get; set; }

        public string? UserIP { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public decimal? Amount { get; set; }

        public int OrderId { get; set; }
    }
}
