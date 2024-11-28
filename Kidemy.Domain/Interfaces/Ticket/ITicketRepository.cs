using Barnamenevisan.Localizing.Repository;

namespace Kidemy.Domain.Interfaces.Ticket
{
    public interface ITicketRepository : IRepository<Models.Ticket.Ticket, int>
    {
        Task<int> GetOwnerUserIdOfTicket(int id);
        Task<int> GetTicketCount();
    }
}
