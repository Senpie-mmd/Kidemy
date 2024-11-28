using Kidemy.Application.ViewModels.Ticket;
using Kidemy.Domain.Models.Ticket;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class TicketMapper
    {
        public static Expression<Func<Ticket, AdminSideTicketViewModel>> MapAdminSideTicketViewModel => (ticket) => new AdminSideTicketViewModel
        {
            Id = ticket.Id,
            CreateDateOnUtc = ticket.CreatedDateOnUtc,
            IsClosed = ticket.IsClosed,
            Priority = ticket.Priority,
            Section = ticket.Section,
            Status = ticket.Status,
            Title = ticket.Title,
            OwnerUserId = ticket.OwnerUserId
        };

        public static Expression<Func<Ticket, ClientSideTicketViewModel>> MapClientSideTicketViewModel => (ticket) => new ClientSideTicketViewModel
        {
            Id = ticket.Id,
            CreateDateOnUtc = ticket.CreatedDateOnUtc,
            IsClosed = ticket.IsClosed,
            Priority = ticket.Priority,
            Section = ticket.Section,
            Status = ticket.Status,
            Title = ticket.Title,
        };

        public static AdminSideTicketDetailsViewModel MapFrom(this AdminSideTicketDetailsViewModel model, Ticket ticket)
        {
            model.Id = ticket.Id;
            model.IsClosed = ticket.IsClosed;
            model.Priority = ticket.Priority;
            model.Section = ticket.Section;
            model.Status = ticket.Status;
            model.Title = ticket.Title;
            model.OwnerUserId = ticket.OwnerUserId;
            model.Messages = ticket.Messages.Select(message => new AdminSideTicketMessageDetailsViewModel().MapFrom(message)).ToList();

            return model;
        }

        public static ClientSideTicketDetailsViewModel MapFrom(this ClientSideTicketDetailsViewModel model, Ticket ticket)
        {
            model.Id = ticket.Id;
            model.IsClosed = ticket.IsClosed;
            model.Priority = ticket.Priority;
            model.Section = ticket.Section;
            model.Status = ticket.Status;
            model.Title = ticket.Title;
            model.OwnerUserId = ticket.OwnerUserId;
            model.Messages = ticket.Messages.Select(message => new ClientSideTicketMessageDetailsViewModel().MapFrom(message)).ToList();

            return model;
        }

        public static CreateTicketViewModel MapFrom(this CreateTicketViewModel model, Ticket ticket)
        {
            model.Id = ticket.Id;
            model.Priority = ticket.Priority;
            model.Section = ticket.Section;
            model.Title = ticket.Title;
            model.OwnerUserId = ticket.OwnerUserId;

            return model;
        }

        public static UpdateTicketViewModel MapFrom(this UpdateTicketViewModel model, Ticket ticket)
        {
            model.Id = ticket.Id;
            model.Priority = ticket.Priority;
            model.Section = ticket.Section;

            return model;
        }

        public static AdminSideTicketMessageDetailsViewModel MapFrom(this AdminSideTicketMessageDetailsViewModel model, TicketMessage ticketMessage)
        {
            model.CreateDateOnUtc = ticketMessage.CreatedDateOnUtc;
            model.Message = ticketMessage.Message;
            model.SeenByClient = ticketMessage.SeenByClient;
            model.SenderId = ticketMessage.SenderId;
            model.Id = ticketMessage.Id;

            return model;
        }

        public static ClientSideTicketMessageDetailsViewModel MapFrom(this ClientSideTicketMessageDetailsViewModel model, TicketMessage ticketMessage)
        {
            model.CreateDateOnUtc = ticketMessage.CreatedDateOnUtc;
            model.Message = ticketMessage.Message;
            model.SeenByClient = ticketMessage.SeenByClient;
            model.SenderId = ticketMessage.SenderId;
            model.Id = ticketMessage.Id;

            return model;
        }
    }
}
