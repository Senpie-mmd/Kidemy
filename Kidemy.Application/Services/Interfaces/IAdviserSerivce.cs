using Kidemy.Application.ViewModels.Consultation.Adviser;
using Kidemy.Domain.Models.Consultation;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IAdviserSerivce
    {
        Task<Result> CreateAdviserAsync(AdminSideUpsertAdviserViewModel model);

        Task<Result> DeleteAdviserAsync(int id);
        Task<Result<Adviser>> GetAdviserById(int id);
        Task<Result> EditAdviserAsync(AdminSideUpsertAdviserViewModel model);
        Task<Result<AdminSideFilterAdvisersViewModel>> FilterAdvisers(AdminSideFilterAdvisersViewModel model);
        Task<Result<ClientSideFilterAdvisersViewModel>> ClientSideFilterAdvisers(ClientSideFilterAdvisersViewModel model);
        Task<Result<AdminSideUpsertAdviserViewModel>> GetForEditAdviserAsync(int id);
        Task<Result<ClientSideAdviserViewModel>> GetAdviserAsync(int id);

    }
}
