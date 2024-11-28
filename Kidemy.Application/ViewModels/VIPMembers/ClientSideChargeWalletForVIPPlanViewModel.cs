using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.VIPMembers
{
    public class ClientSideChargeWalletForVIPPlanViewModel
    {
        public int? UserId { get; set; }
        public string? UserIP { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public decimal? Amount { get; set; }
        public int PlanId { get; set; }

    }
}
