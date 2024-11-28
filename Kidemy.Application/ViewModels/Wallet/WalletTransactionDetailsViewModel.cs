using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Wallet
{
    public class WalletTransactionDetailsViewModel : BaseEntityViewModel<int>
    {
        #region Properties

        public int UserId { get; set; }

        public int? OrderId { get; set; }

        [MaxLength(50)]
        public string IP { get; set; }

        public WalletTransactionType TransactionType { get; set; }

        public WalletTransactionCase TransactionCase { get; set; }

        public WalletTransactionWay TransactionWay { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public bool IsSuccess { get; set; }

        public string? RefId { get; set; }

        [MaxLength(700, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Description { get; set; }

        public DateTime CreatedDateOnUtc { get; set; }
        public int? PlanId { get; set; }
        public int? ConsulationRequestId { get; set; }

        #endregion
    }
}
