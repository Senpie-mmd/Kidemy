using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Enums.ContactUs;
using Kidemy.Domain.Interfaces.ContactUs;
using Kidemy.Domain.Models.ContactUs;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.ContactUs
{
    public class ContactUsFormRepository : EfRepository<ContactUsForm, int>, IContactUsFormRepository
    {
        public ContactUsFormRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<int> ChangeState(int id, ContactUsFormState state)
        {
            return await _dbSet.ExecuteUpdateAsync(s => s.SetProperty(c => c.State, state));
        }

        public async Task<int> GetContactUsFormCount()
        {
            return await _dbSet.CountAsync(c => c.State != ContactUsFormState.Response);
        }
    }
}
