namespace Kidemy.Application.ViewModels.VIPPlan
{
    public class ClientSideBuyVIPPlanViewModel
    {
        public int? UserId { get; set; }
        public string? UserIP { get; set; }

        public int PlanId { get; set; }
        public bool UseFromWalletBalnace { get; set; }
    }
}
