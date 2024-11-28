using Kidemy.Application.ViewModels.Wallet;

namespace Kidemy.Application.ViewModels.Payment
{
    public class VerifyPaymentResponseViewModel
    {
        public WalletTransactionDetailsViewModel Transaction { get; set; }

        public string RefId { get; set; }
    }
}