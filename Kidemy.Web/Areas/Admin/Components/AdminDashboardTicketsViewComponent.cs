using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class AdminDashboardTicketsViewComponent : ViewComponent
    {
        #region Fields

        private readonly ITicketService _ticketService;

        #endregion

        #region Constructor

        public AdminDashboardTicketsViewComponent(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _ticketService.FilterForAdminAsync(new AdminSideFilterTicketViewModel
            {
                TakeEntity = 5,
                Status = Domain.Enums.Ticket.TicketStatus.PendingAdmin,

            });

            return View("AdminDashboardTickets", model.Value);
        }

        #endregion

    }
}
