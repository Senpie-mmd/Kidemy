using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Models.VIPMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.Master
{
    public class MasterContract : AuditBaseEntity<int>
    {
        public string Title { get; set; }
        public string FileName { get; set; }
        public bool IsPublished { get; set; }

        #region Relations

        public ICollection<UploadedMasterContract> UploadedMasterContracts { get; set; }

        #endregion
    }
}
