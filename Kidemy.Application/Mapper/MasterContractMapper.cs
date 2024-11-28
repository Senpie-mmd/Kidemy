using Kidemy.Application.ViewModels.MasterContract;
using Kidemy.Domain.Models.Master;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class MasterContractMapper
    {
        public static Expression<Func<MasterContract, AdminSideMasterContractViewModel>> MapMasterContractViewModel => (MasterContract masterContract) =>
          new AdminSideMasterContractViewModel()
          {
              Id = masterContract.Id,
              Title = masterContract.Title,
              FileName = masterContract.FileName,
              IsPublished = masterContract.IsPublished
          };

        public static AdminSideUpdateMasterContractViewModel MapFrom(this AdminSideUpdateMasterContractViewModel model, MasterContract masterContract)
        {
            model.Id = masterContract.Id;
            model.Title = masterContract.Title;
            model.FileName = masterContract.FileName;
            model.IsPublished = masterContract.IsPublished;

            return model;
        }

        public static ClientSideMasterContractDetailsViewModel MapFrom(this ClientSideMasterContractDetailsViewModel model, MasterContract masterContract)
        {
            model.Id = masterContract.Id;
            model.Title = masterContract.Title;
            model.FileName = masterContract.FileName;
            model.IsPublished = masterContract.IsPublished;

            return model;
        }
    }
}
