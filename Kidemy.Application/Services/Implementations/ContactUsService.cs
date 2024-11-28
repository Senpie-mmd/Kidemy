using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.ContactUs;
using Kidemy.Domain.Events.ContactUs;
using Kidemy.Domain.Interfaces.ContactUs;
using Kidemy.Domain.Models.ContactUs;
using Kidemy.Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Services.Implementations
{
    public class ContactUsService : IContactUsService
    {
        #region Fields

        private readonly IContactUsRepository _contactUsRepository;
        private readonly ILogger<ContactUsService> _logger;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Ctor

        public ContactUsService(IContactUsRepository contactUsRepository, ILogger<ContactUsService> logger, IMediatorHandler mediatorHandler)
        {
            _contactUsRepository = contactUsRepository;
            _logger = logger;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        public async Task<Result<AdminSideUpsertContactUsViewModel>> GetContactUs()
        {
            var contactUs = await _contactUsRepository.GetAllAsync();

            return new AdminSideUpsertContactUsViewModel().MapFrom(contactUs.FirstOrDefault()!);
        }

        public async Task<Result> UpsertContactUs(AdminSideUpsertContactUsViewModel model)
        {
            if (model is null)
                return Result.Failure(ErrorMessages.NullValue);

            var contactUs = (await _contactUsRepository.GetAllAsync()).FirstOrDefault();
            var copiedContactUs = contactUs.DeepCopy<ContactUs>();

            contactUs.Address = model.Address;
            contactUs.Email = model.Email;
            contactUs.Mobile = model.Mobile;
            contactUs.UpdatedDateOnUtc = DateTime.UtcNow;

            _contactUsRepository.Update(contactUs);
            await _contactUsRepository.SaveAsync();


            await _mediatorHandler.PublishEvent(new ContactUsUpdatedEvent(
                contactUs.Id, contactUs.Address, contactUs.Mobile, contactUs.Email,
                copiedContactUs.Address,copiedContactUs.Mobile,copiedContactUs.Email
                ));

            return Result.Success(SuccessMessages.ContactUsUpdatedSuccessfully);

        }

    }
}
