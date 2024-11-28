using Kidemy.Application.ViewModels.Banner;
using Kidemy.Domain.Models.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class BannerMapper
    {

        public static AdminSideUpdateBannerViewModel MapFrom(this AdminSideUpdateBannerViewModel model, Banner Banner)
        {
            model.Id = Banner.Id;
            model.IsPublished = Banner.IsPublished;
            model.ImageName = Banner.ImageName;
            model.Place = Banner.Place;
            model.URL = Banner.URL;

            return model;
        }
        public static ClientSideBannerViewModel MapFrom(this ClientSideBannerViewModel model, Banner Banner)
        {
            model.Id = Banner.Id;
            model.IsPublished = Banner.IsPublished;
            model.ImageName = Banner.ImageName;
            model.Place = Banner.Place;
            model.URL = Banner.URL;

            return model;
        }
        
        public static Expression<Func<Banner, AdminSideBannerViewModel>> MapBannerViewModel => (Banner Banner) =>
          new AdminSideBannerViewModel
          {
              Id = Banner.Id,
              ImageName = Banner.ImageName,
              URL = Banner.URL,
              Place = Banner.Place,
              IsPublished = Banner.IsPublished
          };
    }
}
