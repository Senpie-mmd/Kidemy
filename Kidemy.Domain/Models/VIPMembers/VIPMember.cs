using Barnamenevisan.Localizing.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.VIPMembers
{
    public class VIPMember : AuditBaseEntity<int>
    {
        public int UserId { get; set; }
        public int VIPPlanId { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }

        #region Relations
        public VIPPlan VIPPlan { get; set; }

        #endregion

    }
}
