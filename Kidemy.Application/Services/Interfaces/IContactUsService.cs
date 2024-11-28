using Kidemy.Application.ViewModels.ContactUs;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IContactUsService
    {
        Task<Result<AdminSideUpsertContactUsViewModel>> GetContactUs();
        Task<Result> UpsertContactUs(AdminSideUpsertContactUsViewModel model);
    }
}
