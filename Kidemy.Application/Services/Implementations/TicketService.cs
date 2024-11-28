using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mediator;
using Kidemy.Application.Tools;
using Kidemy.Domain.Enums.Ticket;
using Kidemy.Domain.Interfaces.Ticket;
using Kidemy.Domain.Models.Ticket;
using Kidemy.Domain.Shared;
using Kidemy.Application.Mapper;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Ticket;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Kidemy.Domain.Events.Ticket;

namespace Kidemy.Application.Services.Implementations
{
    public class TicketService : ITicketService
    {
        #region Fields

        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<TicketService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor

        public TicketService(ITicketRepository ticketRepository,
            ILogger<TicketService> logger,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IMediatorHandler mediatorHandler)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods

        public async Task<Result> CreateByAdminAsync(CreateTicketViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.OwnerUserId is null) return Result.Failure(ErrorMessages.UserIsRequiredError);

            var ticket = new Ticket
            {
                OwnerUserId = model.OwnerUserId.Value,
                Priority = model.Priority.Value,
                Section = model.Section.Value,
                Status = TicketStatus.PendingUser,
                Title = model.Title,

                Messages = new List<TicketMessage>
                {
                    new TicketMessage
                    {
                        SenderId = _httpContextAccessor.HttpContext.User.GetUserId(),
                        Message = model.Message
                    }
                }
            };

            await _ticketRepository.InsertAsync(ticket);
            await _ticketRepository.SaveAsync();

            var createTicketEvent = new TicketCreatedEvent(

                ticket.Title,
                ticket.OwnerUserId,
                ticket.Status,
                ticket.Priority,
                ticket.Section,
                ticket.Messages
            );

            await _mediatorHandler.PublishEvent(createTicketEvent);

            return Result.Success();
        }

        public async Task<Result> CreateByUserAsync(CreateTicketViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var ticket = new Ticket
            {
                OwnerUserId = model.OwnerUserId.Value,
                Priority = model.Priority.Value,
                Section = model.Section.Value,
                Status = TicketStatus.PendingAdmin,
                Title = model.Title,

                Messages = new List<TicketMessage>
                {
                    new TicketMessage
                    {
                        SenderId = model.OwnerUserId.Value,
                        Message = model.Message
                    }
                }
            };

            await _ticketRepository.InsertAsync(ticket);
            await _ticketRepository.SaveAsync();

            var createTicketEvent = new TicketCreatedByUserEvent(

                ticket.Id,
                ticket.Title,
                ticket.OwnerUserId,
                ticket.Status,
                ticket.Priority,
                ticket.Section,
                ticket.Messages
            );

            await _mediatorHandler.PublishEvent(createTicketEvent);

            return Result.Success();
        }

        public async Task<Result<AdminSideFilterTicketViewModel>> FilterForAdminAsync(AdminSideFilterTicketViewModel model)
        {
            if (model is null) return Result.Failure<AdminSideFilterTicketViewModel>(ErrorMessages.ProcessFailedError);

            var conditions = Filter.GenerateConditions<Ticket>();

            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                conditions.Add(t => t.Title.Contains(model.Title));
            }

            if (!string.IsNullOrWhiteSpace(model.Message))
            {
                conditions.Add(t => t.Messages.Any(m => m.Message.Contains(model.Message)));
            }

            if (model.OwnerUserId is not null)
            {
                conditions.Add(t => t.OwnerUserId == model.OwnerUserId);
            }

            if (model.Priority is not null)
            {
                conditions.Add(t => t.Priority == model.Priority);
            }

            if (model.Section is not null)
            {
                conditions.Add(t => t.Section == model.Section);
            }

            if (model.Status is not null)
            {
                conditions.Add(t => t.Status == model.Status);
            }

            if (model.IsClosed is not null)
            {
                conditions.Add(t => t.IsClosed == model.IsClosed);
            }

            await _ticketRepository.FilterAsync(model, conditions, TicketMapper.MapAdminSideTicketViewModel);

            var userIds = model.Entities.Select(m => m.OwnerUserId).ToList();

            var fullNames = await _userService.GetUsersFullNames(userIds);

            foreach (var ticket in model.Entities)
            {
                var fullName = fullNames.First(f => f.UserId == ticket.OwnerUserId);

                ticket.UserFullName = fullName.UserFullName;
            }

            return model;
        }

        public async Task<Result<ClientSideFilterTicketViewModel>> FilterForClientAsync(ClientSideFilterTicketViewModel model)
        {
            if (model is null) return Result.Failure<ClientSideFilterTicketViewModel>(ErrorMessages.ProcessFailedError);

            var conditions = Filter.GenerateConditions<Ticket>();

            if (model.OwnerUserId is not null)
            {
                conditions.Add(t => t.OwnerUserId == model.OwnerUserId);
            }

            await _ticketRepository.FilterAsync(model, conditions, TicketMapper.MapClientSideTicketViewModel);

            return model;
        }

        public async Task<Result<AdminSideTicketDetailsViewModel>> GetDetailsForAdminAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id, nameof(Ticket.Messages));

            if (ticket is null) return Result.Failure<AdminSideTicketDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideTicketDetailsViewModel().MapFrom(ticket);

            return model;
        }

        public async Task<Result<ClientSideTicketDetailsViewModel>> GetDetailsForClientAsync(int id, int userId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id, nameof(Ticket.Messages));

            if (ticket is null) return Result.Failure<ClientSideTicketDetailsViewModel>(ErrorMessages.ProcessFailedError);


            if (ticket.OwnerUserId != userId) return Result.Failure<ClientSideTicketDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var model = new ClientSideTicketDetailsViewModel().MapFrom(ticket);

            return model;
        }

        public async Task<Result> UpdateTicketAsync(UpdateTicketViewModel model)
        {
            var ticket = await _ticketRepository.GetByIdAsync(model.Id);

            var copiedTicket = ticket?.DeepCopy<Ticket>();

            if (ticket is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            ticket.Priority = model.Priority;
            ticket.Section = model.Section;
            ticket.UpdatedDateOnUtc = DateTime.UtcNow;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveAsync();

            var updateTicketEvent = new TicketUpdatedEvent(
                ticket.Id,
                copiedTicket.Status,
                ticket.Status,
                copiedTicket.Priority,
                ticket.Priority,
                copiedTicket.Section,
                ticket.Section
                );

            await _mediatorHandler.PublishEvent(updateTicketEvent);

            return Result.Success();
        }

        public async Task<Result<UpdateTicketViewModel>> GetForEditByIdAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket is null) return Result.Failure<UpdateTicketViewModel>(ErrorMessages.ProcessFailedError);

            var model = new UpdateTicketViewModel().MapFrom(ticket);

            return model;
        }

        public async Task<Result> AddTicketMessageByAdmin(AddTicketMessageViewModel model)
        {
            var ticket = await _ticketRepository.GetByIdAsync(model.TicketId, includeProperties: nameof(Ticket.Messages));

            if (ticket is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (ticket.Messages == null) ticket.Messages = new List<TicketMessage>();

            ticket.Status = TicketStatus.Answered;
            ticket.UpdatedDateOnUtc = DateTime.UtcNow;

            ticket.Messages.Add(new TicketMessage
            {
                Message = model.Message,
                SenderId = model.SenderId,
                CreatedDateOnUtc = DateTime.UtcNow,
                UpdatedDateOnUtc = DateTime.UtcNow
            });

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveAsync();

            var createTicketEvent = new TicketCreatedByUserEvent(

                ticket.Id,
                ticket.Title,
                ticket.OwnerUserId,
                ticket.Status,
                ticket.Priority,
                ticket.Section,
                ticket.Messages
            );

            await _mediatorHandler.PublishEvent(createTicketEvent);

            return Result.Success();
        }

        public async Task<Result> AddTicketMessageByUser(AddTicketMessageViewModel model)
        {
            var ticket = await _ticketRepository.GetByIdAsync(model.TicketId, includeProperties: nameof(Ticket.Messages));

            if (ticket is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (ticket.Messages == null) ticket.Messages = new List<TicketMessage>();

            ticket.Status = TicketStatus.PendingAdmin;
            ticket.UpdatedDateOnUtc = DateTime.UtcNow;

            ticket.Messages.Add(new TicketMessage
            {
                Message = model.Message,
                SenderId = model.SenderId,
                CreatedDateOnUtc = DateTime.UtcNow,
                UpdatedDateOnUtc = DateTime.UtcNow
            });

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveAsync();

            var addTicketMessageEvent = new TicketMessageAddedByUserEvent(

                ticket.Id,
                model.SenderId,
                model.Message
            );

            await _mediatorHandler.PublishEvent(addTicketMessageEvent);

            return Result.Success();
        }

        public async Task<Result> ChangeToSeenAllTicketMessages(int ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId, nameof(Ticket.Messages));

            if (ticket is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var adminMessages = ticket.Messages
                    .Where(m => m.SenderId != ticket.OwnerUserId)
                    .ToList();

            foreach (var message in adminMessages)
            {
                message.SeenByClient = true;
            }

            ticket.UpdatedDateOnUtc = DateTime.UtcNow;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<int>> GetTicketCount()
        {
            return await _ticketRepository.GetTicketCount();
        }

        public async Task<Result> CloseTicket(int id)
        {
            if (id is < 1) return Result.Failure(ErrorMessages.FailedOperationError);

            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket is null) return Result.Failure(ErrorMessages.FailedOperationError);

            ticket.IsClosed = true;
            ticket.UpdatedDateOnUtc = DateTime.UtcNow;
            ticket.Status = TicketStatus.Closed;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveAsync();

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion

    }
}
