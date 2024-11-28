using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.MasterNotification;
using Kidemy.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.MasterNotification
{
    internal class NotificationMasterMappingRepository : EfRepository<Domain.Models.MasterNotification.NotificationMasterMapping, int>, INotificationMasterMappingRepository
    {
        #region Constructor
        public NotificationMasterMappingRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
