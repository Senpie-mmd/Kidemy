using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Master;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.UploadedMasterContract
{
    public class ClientSideUploadedMasterContractDetailsViewModel : BaseEntityViewModel<int>
    {
        public int MasterId { get; set; }
        public int MasterContractId { get; set; }
        public UploadedMasterContractStatus Status { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
    }
}
