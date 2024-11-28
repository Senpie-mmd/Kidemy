using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.SettlementTransaction
{
    public class ClientSideFilterSettlementTransactionViewModel : BasePaging<ClientSideSettlementTransactionViewModel>
    {
        [Display(Name = "AccountNumber")]
        public string? AccountNumber { get; set; }

        [Display(Name = "CardNumber")]
        public string? CardNumber { get; set; }

        public int? UserId { get; set; }

    }
}
