using Kidemy.Application.ViewModels.Master;
using Kidemy.Application.ViewModels.MasterNotification;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Application.ViewModels.Slider;
using Kidemy.Domain.Models.Master;
using Kidemy.Domain.Models.MasterNotification;
using Kidemy.Domain.Models.Page;
using Kidemy.Domain.Models.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class MasterNotificationMapper
    {
        public static ClientSideMasterNotificationViewModel MapFrom(this ClientSideMasterNotificationViewModel model, MasterNotification masterNotification)
        {
            var fileExtension = Path.GetExtension(masterNotification.DemoVideoFileName);

            if (fileExtension !=null)
            {
                model.FileNameSuffix = fileExtension.ToUpper();
            }
            model.Id = masterNotification.Id;
            model.CreateDate = masterNotification.CreatedDateOnUtc;
            model.SenderId = masterNotification.SenderId;
            model.Message = masterNotification.Message;
            model.DemoVideoFileName = masterNotification.DemoVideoFileName;
  
            return model;
        }

        public static Expression<Func<MasterNotification, AdminSideMasterNotificationViewModel>> AdminMapMasterNotificationViewModel => (MasterNotification masterNotification) => new AdminSideMasterNotificationViewModel
        {
            Id = masterNotification.Id,
            Message = masterNotification.Message,
            DemoVideoFileName = masterNotification.DemoVideoFileName,
            SenderId = masterNotification.SenderId,
        };

        public static AdminSideUpdateMasterNotificationViewModel MapFrom(this AdminSideUpdateMasterNotificationViewModel model, MasterNotification masterNotification)
        {
            model.Id = masterNotification.Id;
            model.Message = masterNotification.Message;
            model.DemoVideoFileName = masterNotification.DemoVideoFileName;


            return model;
        }
    }
}
