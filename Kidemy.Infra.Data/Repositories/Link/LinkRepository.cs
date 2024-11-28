using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Link;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.Link
{
    public class LinkRepository : EfRepository<Domain.Models.Link.Link, int>, ILinkRepository
    {
        #region Constructor
        public LinkRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
