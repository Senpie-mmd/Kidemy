using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Models.WithdrawRequest
{
    public enum WithdrawRequestStatus
    {
        [Display(Name = "Pending")]
        Pending = 0,

        [Display(Name = "Accepeted")]
        Accepeted = 1,

        [Display(Name = "Rejected")]
        Rejected = 2
    }
}
