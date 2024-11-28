using Kidemy.Domain.Shared;
using Kidemy.Application.ViewModels.Ticket;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ITicketService
    {
        Task<Result> AddTicketMessageByAdmin(AddTicketMessageViewModel model);
        Task<Result> AddTicketMessageByUser(AddTicketMessageViewModel model);
        Task<Result> ChangeToSeenAllTicketMessages(int ticketId);
        Task<Result> CreateByAdminAsync(CreateTicketViewModel model);
        Task<Result> CreateByUserAsync(CreateTicketViewModel model);
        Task<Result> UpdateTicketAsync(UpdateTicketViewModel model);
        Task<Result<AdminSideFilterTicketViewModel>> FilterForAdminAsync(AdminSideFilterTicketViewModel model);
        Task<Result<UpdateTicketViewModel>> GetForEditByIdAsync(int id);
        Task<Result<AdminSideTicketDetailsViewModel>> GetDetailsForAdminAsync(int id);
        Task<Result<ClientSideTicketDetailsViewModel>> GetDetailsForClientAsync(int id, int userId);
        Task<Result<int>> GetTicketCount();
        Task<Result> CloseTicket(int id);
        Task<Result<ClientSideFilterTicketViewModel>> FilterForClientAsync(ClientSideFilterTicketViewModel model);
    }
}