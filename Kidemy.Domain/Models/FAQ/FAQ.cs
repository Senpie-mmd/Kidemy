using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.FAQ
{
    public class FAQ : AuditBaseEntity<int>
    {
        #region Properties 
        public string Title { get; set; }
        public string Answer { get; set; }

        #endregion
    }
}
