using Kidemy.Application.ViewModels.MasterContract;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Application.ViewModels.UploadedMasterContract;
using Kidemy.Domain.Enums.Master;
using Kidemy.Domain.Models.Master;
using Kidemy.Domain.Models.Page;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class UploadedMasterContractMapper
    {
        public static ClientSideUploadedMasterContractDetailsViewModel MapFrom(this ClientSideUploadedMasterContractDetailsViewModel model, UploadedMasterContract uploadedMasterContract)
        {
            model.Id = uploadedMasterContract.Id;
            model.MasterId = uploadedMasterContract.MasterId;
            model.MasterContractId = uploadedMasterContract.MasterContractId;
            model.Status = uploadedMasterContract.Status;
            model.FileName = uploadedMasterContract.FileName;

            return model;
        }

        public static ClientSideUpdateUploadedMasterContractViewModel MapFrom(this ClientSideUpdateUploadedMasterContractViewModel model, UploadedMasterContract uploadedMasterContract)
        {
            model.Id = uploadedMasterContract.Id;
            model.MasterId = uploadedMasterContract.MasterId;
            model.MasterContractId = uploadedMasterContract.MasterContractId;
            model.Status = uploadedMasterContract.Status;
            model.FileName = uploadedMasterContract.FileName;

            return model;
        }

        public static Expression<Func<UploadedMasterContract, AdminSideMasterInformationForContractsPendingConfirmationViewModel>> MapMasterInformationUploadedMasterContractViewModel => (UploadedMasterContract uploadedMasterContract) =>
          new AdminSideMasterInformationForContractsPendingConfirmationViewModel()
          {
              //Id = uploadedMasterContract.Id,
              MasterId = uploadedMasterContract.MasterId,
             // MasterContractId = uploadedMasterContract.MasterContractId,
             // Status = uploadedMasterContract.Status,
              //FileName = uploadedMasterContract.FileName
          };

        public static AdminSideUpdateUploadedMasterContractViewModel MapFrom(this AdminSideUpdateUploadedMasterContractViewModel model, UploadedMasterContract uploadedMasterContract)
        {
            model.UploadedMasterContractId = uploadedMasterContract.Id;
            model.MasterId = uploadedMasterContract.MasterId;
            model.MasterContractId = uploadedMasterContract.MasterContractId;
            model.Status = uploadedMasterContract.Status;

            return model;
        }

        public static AdminSideMasterContractsPendingConfirmationDetailViewModel MapFrom(this AdminSideMasterContractsPendingConfirmationDetailViewModel model, UploadedMasterContract uploadedMasterContract)
        {
            model.UploadedMasterContractId = uploadedMasterContract.Id;
            model.Status = uploadedMasterContract.Status;
            model.FileName = uploadedMasterContract.FileName;
            model.Title = uploadedMasterContract.MasterContract.Title;

            return model;
        }

    }
}
