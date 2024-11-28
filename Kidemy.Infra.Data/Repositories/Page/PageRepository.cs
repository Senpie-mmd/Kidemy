using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Page;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Page
{
    public class PageRepository : EfRepository<Domain.Models.Page.Page, int>, IPageRepository
    {
        public PageRepository(KidemyContext context) : base(context)
        {

        }

    }
}
