using Barnamenevisan.Localizing.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.Newsletter
{
    public class Newsletter : AuditBaseEntity<int>
    {
        #region Properties 

        public string Email { get; set; }
        public string Ip { get; set; }

        #endregion
    }
}
