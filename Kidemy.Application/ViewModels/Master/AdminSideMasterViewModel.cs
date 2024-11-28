using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Master;

namespace Kidemy.Application.ViewModels.Master
{
    public class AdminSideMasterViewModel : BaseEntityViewModel<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
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
        public MasterStatus Status { get; set; }
    }
}
