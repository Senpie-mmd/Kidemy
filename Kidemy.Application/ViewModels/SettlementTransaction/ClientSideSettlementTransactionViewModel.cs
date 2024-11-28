using Barnamenevisan.Localizing.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.SettlementTransaction
{
    public class ClientSideSettlementTransactionViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "TransactionDate")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "CardNumber")]
        public string CardNumber { get; set; }

        [Display(Name = "AccountNumber")]
        public string AccountNumber { get; set; }

        [Display(Name = "TrackingCode")]
        public string TrackingCode { get; set; }

        public int UserId { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateDateUTC { get; set; }

        public DateTime UpdateDateUTC { get; set; }
    }
}
