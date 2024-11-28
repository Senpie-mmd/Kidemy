using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.SettlementTransaction
{
    public class AdminSideUpsertSettlementTransactionViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "TransactionDate")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string TransactionDate { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public long Price { get; set; }

        [Display(Name = "CardNumber")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string CardNumber { get; set; }

        [Display(Name = "AccountNumber")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string AccountNumber { get; set; }

        public int UserId { get; set; }

        [Display(Name = "TrackingCode")]
        public string TrackingCode { get; set; }

    }
}
