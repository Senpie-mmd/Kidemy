using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.VIPPlan
{
    public class AdminSideVIPPlanViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Title")]
        [Translate(EntityName = nameof(Domain.Models.VIPMembers.VIPPlan))]
        public string Title { get; set; }

        [Display(Name = "DurationDay")]
        public int DurationDay { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
}
