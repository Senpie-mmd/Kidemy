using Barnamenevisan.Localizing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.DTOs.User
{
    public class AdminSideUserNameAndMobileForMasterModel : BaseEntityViewModel<int>
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserMobile { get; set; }
    }
}
