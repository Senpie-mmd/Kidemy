namespace Kidemy.Application.ViewModels.VIPMembers
{
    public class ClientSideVIPUserInformationViewModel
    {
        public int UserId { get; set; }
        public int VIPPlanId { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
        public bool IsVIPUser { get; set; }
    }
}
