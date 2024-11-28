using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.AboutUs;
using Kidemy.Domain.Interfaces.SiteSetting;
using Kidemy.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.SiteSetting
{
    public class SiteSettingRepository : EfRepository<Domain.Models.SiteSetting.SiteSetting, int>, ISiteSettingRepository
    {
        public SiteSettingRepository(KidemyContext context) : base(context)
        {

        }
    }
}
