using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.ContactUs;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.ContactUs
{
    public class ContactUsRepository : EfRepository<Domain.Models.ContactUs.ContactUs, int>, IContactUsRepository
    {
        public ContactUsRepository(KidemyContext context) : base(context)
        {
        }
    }
}
