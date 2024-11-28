using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Wallet
{
    public enum WalletTransactionType
    {
        [Display(Name = "Withdraw")]
        Withdraw,
        
        [Display(Name = "Deposit")]
        Deposit
    }
}
