using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Wallet;

namespace Kidemy.Domain.Models.Wallet
{
    public class WalletTransaction : AuditBaseEntity<int>
    {
        #region Properties

        public int UserId { get; set; }

        public int? OrderId { get; set; }

        public int? CourseId { get; set; }

        public string IP { get; set; }

        public WalletTransactionType TransactionType { get; set; }

        public WalletTransactionCase TransactionCase { get; set; }

        public WalletTransactionWay TransactionWay { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public bool IsSuccess { get; set; }

        public string? Description { get; set; }

        public string? RefId { get; set; }

        public int? PlanId { get; set; }
        public int? ConsulationRequestId { get; set; }

        #endregion


    }
}
