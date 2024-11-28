using Barnamenevisan.Localizing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.Master
{
    public class ClientSideMasterViewModel : BaseEntityViewModel<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarName { get; set; }
        public string Bio { get; set; }
    }
}
