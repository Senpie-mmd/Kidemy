using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Models.WithdrawRequest;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.WithrawRequest
{
    public class FilterWithdrawRequestViewModel : BasePaging<WithdrawRequestViewModel>
    {
        [Display(Name = "UserName")]
        public string? UserName { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "WithdrawRequestStatus")]
        public WithdrawRequestStatus? Status { get; set; }
    }
}
