using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.ContactUs;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.Enums.ContactUs;
using Kidemy.Domain.Events.ContactUs;
using Kidemy.Domain.Interfaces.ContactUs;
using Kidemy.Domain.Models.ContactUs;
using Kidemy.Domain.Models.Wallet;
using Kidemy.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Services.Implementations
{
    public class ContactUsFormService : IContactUsFormService
    {
        #region Fields

        private readonly IContactUsFormRepository _contactUsFormRepository;
        private readonly ILogger<ContactUsFormService> _logger;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IEmailService _emailService;
        private readonly IViewRenderService _viewRenderService;

        #endregion

        #region Ctor

        public ContactUsFormService(IContactUsFormRepository contactUsFormRepository, ILogger<ContactUsFormService> logger, IMediatorHandler mediatorHandler, IEmailService emailService, IViewRenderService viewRenderService)
        {
            _contactUsFormRepository = contactUsFormRepository;
            _logger = logger;
            _mediatorHandler = mediatorHandler;
            _emailService = emailService;
            _viewRenderService = viewRenderService;
        }

        #endregion

        public async Task<Result<FilterContactUsFormViewModel>> FilterContactUsForm(FilterContactUsFormViewModel filter)
        {
            if (filter is null) return Result.Failure<FilterContactUsFormViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<ContactUsForm>();

            switch (filter.State)
            {
                case Domain.Enums.ContactUs.ContactUsStateForFilter.All:
                    filterConditions.Add(t => t.State == ContactUsFormState.Answered || t.State == ContactUsFormState.NotAnswered);
                    break;
                case Domain.Enums.ContactUs.ContactUsStateForFilter.Answered:
                    filterConditions.Add(t => t.State == ContactUsFormState.Answered);
                    break;
                case Domain.Enums.ContactUs.ContactUsStateForFilter.NotAnswered:
                    filterConditions.Add(t => t.State == ContactUsFormState.NotAnswered);
                    break;

            }

            await _contactUsFormRepository.FilterAsync(filter, filterConditions, ContactUsMapper.MapContactUsFormViewModel);

            return filter;
        }

        public async Task<Result<AdminSideContactUsFormViewModel>> GetContactUsFormById(int id)
        {
            if (id <= 0)
                return Result.Failure<AdminSideContactUsFormViewModel>(ErrorMessages.NullValue);

            var contactUsForm = await _contactUsFormRepository.GetByIdAsync(id);

            if (contactUsForm is null)
            {
                _logger.LogError($"Contact us form with id:{id} not found");
                return Result.Failure<AdminSideContactUsFormViewModel>(ErrorMessages.ItemNotFoundError);
            }

            var model = new AdminSideContactUsFormViewModel().MapFrom(contactUsForm);

            if (contactUsForm.State == ContactUsFormState.Answered)
            {
                model.AnswerText = (await _contactUsFormRepository.GetAllAsync(c => c.ParentId == contactUsForm.Id))
                    .FirstOrDefault()!.Description;
            }

            return model;

        }

        public async Task<Result> CreateContactUsForm(ClientSideCreateContactUsFormViewModel model)
        {
            if (model is null)
                return Result.Failure(ErrorMessages.NullValue);

            await _contactUsFormRepository.InsertAsync(new ContactUsForm
            {
                CreateDate = DateTime.UtcNow,
                Email = model.Email,
                Description = model.Description,
                FullName = model.FullName,
                IsDeleted = false,
                State = Domain.Enums.ContactUs.ContactUsFormState.NotAnswered,
                UserId=model.UserId,

            });
            await _contactUsFormRepository.SaveAsync();

            await _mediatorHandler.PublishEvent(new ContactUsFormCreatedEvent(model.FullName, model.Email, model.Description, model.UserId));

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> AnswerContactUsForm(AdminSideAnswerContactUsFormViewModel model)
        {
            if (model is null)
                return Result.Failure(ErrorMessages.NullValue);

            var contactUsForm = await _contactUsFormRepository.GetByIdAsync(model.ContactUsFormId);

            if (contactUsForm is null)
            {
                _logger.LogError($"Contact us form with id:{model.ContactUsFormId} not found for reply");
                return Result.Failure(ErrorMessages.ItemNotFoundError);
            }

            #region SendEmail

            var body = await _viewRenderService.RenderToStringAsync("Email/EmailNotification", model.Answer);

            await _emailService.SendEmailAsync(contactUsForm.Email, "پاسخ به ارتباط با ما", body);

            #endregion


            contactUsForm.State = ContactUsFormState.Answered;
            contactUsForm.Parent = new ContactUsForm
            {
                AnswererId = model.AnswererId,
                CreateDate = DateTime.UtcNow,
                Description = model.Answer,
                ParentId = model.ContactUsFormId,
                Email = "",
                FullName = "",
                IsDeleted = false,
                State = Domain.Enums.ContactUs.ContactUsFormState.Response,
                UserId=model.AnswererId

            };
            _contactUsFormRepository.Update(contactUsForm);
            await _contactUsFormRepository.SaveAsync();

            await _mediatorHandler.PublishEvent(new ContactUsFormAnsweredEvent(model.ContactUsFormId));

            return Result.Success(SuccessMessages.SuccessfullyDone); ;
        }

        public async Task<Result> ChangeStateContactUsForm(int id, ContactUsFormState state)
        {
            if (id <= 0 || state < 0)
                return Result.Failure(ErrorMessages.NullValue);

            await _contactUsFormRepository.ChangeState(id, state);
            await _contactUsFormRepository.SaveAsync();

            await _mediatorHandler.PublishEvent(new ContactUsFormAnsweredEvent(id));

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> DeleteContactUsForm(int id)
        {
            if (id <= 0)
                return Result.Failure(ErrorMessages.NullValue);

            var contactUsForm = await _contactUsFormRepository.GetByIdAsync(id);

            if (contactUsForm is null)
            {
                _logger.LogError($"Contact us form with id:{id} not found for reply");
                return Result.Failure(ErrorMessages.ItemNotFoundError);
            }

            contactUsForm.IsDeleted = true;
            _contactUsFormRepository.Update(contactUsForm);
            await _contactUsFormRepository.SaveAsync();

            await _mediatorHandler.PublishEvent(new ContactUsFormDeletedEvent(id));

            return Result.Success(SuccessMessages.SuccessfullyDone); ;
        }

        public async Task<int> GetContactUsFormCount()
        {
            return await _contactUsFormRepository.GetContactUsFormCount();
        }
    }
}
