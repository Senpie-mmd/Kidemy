using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.VIPPlan
{
    public class AdminSideFilterVIPPlanViewModel : BasePaging<AdminSideVIPPlanViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
    }
}
