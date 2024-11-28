using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.BankAccount
{
    public enum BankAccountCardStatus
    {

        [Display(Name = "All")]
        All,
        [Display(Name = "Pending")]
        Pending,

        [Display(Name = "Accepeted")]
        Accepeted,

        [Display(Name = "Rejected")]
        Rejected
    }
}

