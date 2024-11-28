using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Email;
using Kidemy.Domain.Interfaces;
using Kidemy.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Services.Implementations
{
    public class EmailSettingService : IEmailSettingService
    {
        #region Fields

        private readonly ILogger<EmailSettingService> _logger;

        #endregion

        #region Constructor

        public EmailSettingService(ILogger<EmailSettingService> logger)
        {
            _logger = logger;
        }

        #endregion

       

        #region Get

        public async Task<Result<EmailViewModel>> GetFirstEmail()
        {
           // var entity = await _repository.FirstOrDefaultAsync();

            var viewModel = new EmailViewModel();

            //if (entity == null)
            //{
            //    return Result.Success(viewModel);
            //}

            viewModel.Smtp = "smtp.gmail.com";
            viewModel.Port =587;
            viewModel.From = "giftcardsite1402@gmail.com";
            viewModel.Password = "bxbwjswnatnsgksg";
            viewModel.EnableSsL = true;
            viewModel.Id = 1;
            viewModel.DisplayName = "Kidemy";
            return Result.Success(viewModel);
        }

        #endregion

      

    }
}
