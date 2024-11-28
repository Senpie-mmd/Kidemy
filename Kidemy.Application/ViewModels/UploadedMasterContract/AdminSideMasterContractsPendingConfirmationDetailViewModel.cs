using Kidemy.Domain.Enums.Master;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.UploadedMasterContract
{
    public class AdminSideMasterContractsPendingConfirmationDetailViewModel
    {
        public int UploadedMasterContractId { get; set; }
        public UploadedMasterContractStatus Status { get; set; }
        public string? FileName { get; set; }
        public IFormFile File { get; set; }
        public string Title { get; set; }
    }
}
