using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Master;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Master
{
    public class AdminSideMasterDetailsViewModel : BaseEntityViewModel<int>
    {
        public string Bio { get; set; }
        public string FatherName { get; set; }
        public string NationalIDNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public string PlaceOfIssuance { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string IdentificationFileName { get; set; }

        [Display(Name = "Status")]
        public MasterStatus Status { get; set; }
    }
}
