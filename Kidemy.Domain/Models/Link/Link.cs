using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.Link
{
    public class Link : AuditBaseEntity<int>
    {
        #region Properties 
        public string Title { get; set; }
        public string Address { get; set; }
        public LinkType LinkType { get; set; }

        #endregion

    }
}
