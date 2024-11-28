namespace Kidemy.Application.ViewModels.VIPMembers
{
    public class ClientSideJoinVIPMembersViewModel
    {
        public int UserId { get; set; }
        public int VIPPlanId { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
    }
}
