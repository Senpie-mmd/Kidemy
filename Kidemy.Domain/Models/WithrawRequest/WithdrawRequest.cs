using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kidemy.Domain.Models.WithdrawRequest
{
    public class WithdrawRequest : AuditBaseEntity<int>
    {
        public int UserId { get; set; }

        
        public string? RefId { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public int WalletTransactionId { get; set; }

        public int DestinationBankAccountCardId { get; set; }

        public WithdrawRequestStatus Status { get; set; }

    }
}
