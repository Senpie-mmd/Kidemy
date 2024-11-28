using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Ticket;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Ticket
{
    public class TicketRepository : EfRepository<Domain.Models.Ticket.Ticket, int>, ITicketRepository
    {
        public TicketRepository(KidemyContext context) : base(context)
        {
        }

        public Task<int> GetOwnerUserIdOfTicket(int id)
        {
            return _dbSet.Where(t => t.Id == id).Select(t => t.OwnerUserId).FirstOrDefaultAsync();
        }

        public Task<int> GetTicketCount()
        {
            return _dbSet.CountAsync();
        }
    }
}
