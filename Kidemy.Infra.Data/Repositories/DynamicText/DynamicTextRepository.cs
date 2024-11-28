using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.DynamicText;
using Kidemy.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.DynamicText
{
    public class DynamicTextRepository : EfRepository<Domain.Models.DynamicText.DynamicText, int>, IDynamicTextRepository
    {
        #region Constructor
        public DynamicTextRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
