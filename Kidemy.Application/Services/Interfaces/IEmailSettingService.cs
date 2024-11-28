using Kidemy.Application.ViewModels.Email;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IEmailSettingService
    {
        Task<Result<EmailViewModel>> GetFirstEmail();
    }
}
