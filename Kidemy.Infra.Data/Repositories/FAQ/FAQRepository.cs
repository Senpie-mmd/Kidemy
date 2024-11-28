using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.FAQ;
using Kidemy.Domain.Interfaces.Link;
using Kidemy.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.FAQ
{
    public class FAQRepository : EfRepository<Domain.Models.FAQ.FAQ, int>, IFAQRepository
    {
        #region Constructor
        public FAQRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
