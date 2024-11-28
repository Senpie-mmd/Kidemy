using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Master;
using Microsoft.AspNetCore.Http;

namespace Kidemy.Application.ViewModels.UploadedMasterContract
{
    public class AdminSideUpdateUploadedMasterContractViewModel 
    {
        public int UploadedMasterContractId { get; set; }
        public int? MasterId { get; set; }
        public int? MasterContractId { get; set; }
        public UploadedMasterContractStatus Status { get; set; }
    }
}
