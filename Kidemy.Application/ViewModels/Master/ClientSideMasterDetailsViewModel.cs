using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.Master
{
    public class ClientSideMasterDetailsViewModel : BaseEntityViewModel<int>
    {
        public ClientSideMasterDetailsViewModel()
        {
            AvatarName = SiteTools.DefaultImageName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string FatherName { get; set; }
        public string NationalIDNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public string PlaceOfIssuance { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string IdentificationFileName { get; set; }
    }
}
