using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Application.ViewModels.SettlementTransaction
{
    public class AdminSideFilterSettlementTransactionsViewModel : BasePaging<AdminSideSettlementTransactionViewModel>
    {
        public int? UserId { get; set; }

        public string? AccountNumber { get; set; }

        public string? CardNumber { get; set; }
    }
}
