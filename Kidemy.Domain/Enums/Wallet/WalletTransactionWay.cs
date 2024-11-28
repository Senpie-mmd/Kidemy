using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Wallet
{
    public enum WalletTransactionWay
    {
        [Display(Name = "Online")]
        Online,

        [Display(Name = "CardToCard")]
        CardToCard
    }
}
