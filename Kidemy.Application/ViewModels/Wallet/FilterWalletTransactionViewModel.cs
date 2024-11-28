
using Barnamenevisan.Localizing.Shared;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.ViewModels.Wallet
{
    public class FilterWalletTransactionViewModel : BasePaging<WalletTransactionViewModel>
    {
        public int? UserId { get; set; }
    }
}
