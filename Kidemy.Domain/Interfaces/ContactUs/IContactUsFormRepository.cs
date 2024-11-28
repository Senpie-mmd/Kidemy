using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Enums.ContactUs;
using Kidemy.Domain.Models.ContactUs;

namespace Kidemy.Domain.Interfaces.ContactUs
{
    public interface IContactUsFormRepository : IRepository<ContactUsForm, int>
    {
        Task<int> ChangeState(int id, ContactUsFormState state);

        Task<int> GetContactUsFormCount();

    }
}
