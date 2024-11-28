using Kidemy.Application.ViewModels.ContactUs;
using Kidemy.Domain.Enums.ContactUs;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IContactUsFormService
    {
        Task<Result<FilterContactUsFormViewModel>> FilterContactUsForm(FilterContactUsFormViewModel filter);
        Task<Result<AdminSideContactUsFormViewModel>> GetContactUsFormById(int id);
        Task<Result> CreateContactUsForm(ClientSideCreateContactUsFormViewModel model);
        Task<Result> AnswerContactUsForm(AdminSideAnswerContactUsFormViewModel model);
        Task<Result> DeleteContactUsForm(int id);
        Task<Result> ChangeStateContactUsForm(int id, ContactUsFormState state);

        Task<int> GetContactUsFormCount();

    }
}
