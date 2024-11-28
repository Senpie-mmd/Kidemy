using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Newsletter;
using Kidemy.Domain.Events.Newsletter;
using Kidemy.Domain.Interfaces.Newsletter;
using Kidemy.Domain.Models.Newsletter;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace Kidemy.Application.Services.Implementations
{
	public class NewsletterService : INewsletterService
	{
		#region Fields

		private readonly INewsletterRepository _newsletterRepository;
		private readonly IHttpContextAccessor _httpAccessor;
		private readonly IMediatorHandler _mediatorHandler;
		private readonly ILocalizingService _localizingService;

		#endregion

		#region Ctor
		public NewsletterService(INewsletterRepository newsletterRepository,
			IHttpContextAccessor httpAccessor,
			IMediatorHandler mediatorHandler)
		{
			_newsletterRepository = newsletterRepository;
			_httpAccessor = httpAccessor;
			_mediatorHandler = mediatorHandler;
		}
		#endregion

		#region Methods
		public async Task<Result<bool>> CkeckDuplicateEmailAndPhoneNumber(RegisterNewsletterViewModel newsletters)
		{
			if (newsletters is null) return Result.Failure<bool>(ErrorMessages.ProcessFailedError);

			if (await _newsletterRepository.AnyAsync(n => n.Email == newsletters.Email))
			{
				return Result.Failure<bool>(ErrorMessages.FailedOperationError);

			}
			else
			{
				return Result.Success<bool>(true, SuccessMessages.SuccessfullyDone);
			}
		}

		public async Task<Result<FilterNewsletterViewModel>> FilterAsync(FilterNewsletterViewModel model)
		{
			if (model is null) return Result.Failure<FilterNewsletterViewModel>(ErrorMessages.ProcessFailedError);

			var conditions = Filter.GenerateConditions<Newsletter>();

			if (!string.IsNullOrWhiteSpace(model.Email))
			{
				conditions.Add(n => n.Email.Contains(model.Email));
			}

			if (!string.IsNullOrWhiteSpace(model.Ip))
			{
				conditions.Add(n => n.Ip.Contains(model.Ip));
			}

			await _newsletterRepository.FilterAsync(model, conditions, NewsletterMapper.MapAdminSideNewsletterForAdmin);

			return model;
		}

		public async Task<Result> RegisterNewsletterMembership(RegisterNewsletterViewModel newsletters)
		{
			if (newsletters is null) return Result.Failure(ErrorMessages.FailedOperationError);

			if (newsletters.Email is null) return Result.Failure(ErrorMessages.EmailIsNull);

			if (!newsletters.Email.IsEmail()) return Result.Failure(ErrorMessages.EmailFormatError);

            var result = await CkeckDuplicateEmailAndPhoneNumber(newsletters);
			if (result.IsFailure)
			{
				return Result.Failure(ErrorMessages.DuplicatedEmailError);
			}
			Newsletter news = new Newsletter()
			{
				Email = newsletters.Email,
				Ip = _httpAccessor.HttpContext.GetUserIP()
			};

			await _newsletterRepository.InsertAsync(news);
			await _newsletterRepository.SaveAsync();

			var newsletterCreatedEvent = new NewsletterCreatedEvent(
				news.Email,
				news.Ip);

			await _mediatorHandler.PublishEvent(newsletterCreatedEvent);

			return Result.Success(SuccessMessages.SuccessfullyDone);
		}

		public async Task<Result> RemoveNewsletterMember(int memberId)
		{
			if (memberId is 0) return Result.Failure(ErrorMessages.FailedOperationError);

			var member = await _newsletterRepository.GetByIdAsync(memberId);
			if (member is null) return Result.Failure(ErrorMessages.FailedOperationError);

			member.IsDeleted = true;
			_newsletterRepository.Update(member);
			await _newsletterRepository.SaveAsync();

			var newsletterDeleteEvent = new NewsletterDeletedEvent(memberId);

			await _mediatorHandler.PublishEvent(newsletterDeleteEvent);

			return Result.Success(SuccessMessages.SuccessfullyDone);
		}

		#endregion
	}
}
