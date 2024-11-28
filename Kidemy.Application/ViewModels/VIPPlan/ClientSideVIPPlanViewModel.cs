using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.VIPPlan
{
    public class ClientSideVIPPlanViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(Domain.Models.VIPMembers.VIPPlan))]
        public string Title { get; set; }
        public int DurationDay { get; set; }
        public decimal Price { get; set; }
    }
}
