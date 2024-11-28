using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Wallet
{
    public class WithdrawFromWalletViewModel 
    {
        public int? UserId { get; set; }

        public string? UserIP { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public decimal? Amount { get; set; }

        public int? OrderId { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string? Description { get; set; }

        public int? PlanId { get; set; }
        public int? ConsultationRequestId { get; set; }
    }
}
