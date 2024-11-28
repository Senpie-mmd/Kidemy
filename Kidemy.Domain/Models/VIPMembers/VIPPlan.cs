using Barnamenevisan.Localizing.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.VIPMembers
{
    public class VIPPlan : AuditBaseEntity<int>   
    {
        public string Title { get; set; }
        public int DurationDay { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }

        #region Relations
        public ICollection<VIPMember> VIPMembers { get; set; }

        #endregion
    }
}
