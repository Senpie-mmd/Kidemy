using Barnamenevisan.Localizing.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.Master
{
    public class FilterForClientSideMasterViewModel : BasePaging<ClientSideMasterViewModel>
    {
        public string? FullName { get; set; }
    }
}
