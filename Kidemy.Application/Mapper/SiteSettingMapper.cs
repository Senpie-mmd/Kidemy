using Kidemy.Application.ViewModels.AboutUs;
using Kidemy.Application.ViewModels.SiteSetting;
using Kidemy.Domain.Models.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class SiteSettingMapper
    {
        public static AdminSideUpsertSiteSettingViewModel MapFrom(this AdminSideUpsertSiteSettingViewModel model, SiteSetting siteSetting)
        {
            model.Id = siteSetting.Id;
            model.Email = siteSetting.Email;
            model.Mobile = siteSetting.Mobile;
            model.Mobile2 = siteSetting.Mobile2;
            model.LogoName=siteSetting.LogoName;
            model.Address = siteSetting.Address;
            model.CollectionManagement=siteSetting.CollectionManagement;
            //model.FooterDescription=siteSetting.FooterDescription;
            model.CopyrightDescription = siteSetting.CopyrightDescription;
            model.NewsletterDescription = siteSetting.NewsletterDescription;
            model.WithdrawRequestMinimumAmount = siteSetting.WithdrawRequestMinimumAmount;

            return model;
        }

        public static SiteSettingDetailsViewModel MapFrom(this SiteSettingDetailsViewModel model, SiteSetting siteSetting)
        {
            model.Id = siteSetting.Id;
            model.Email = siteSetting.Email;
            model.Mobile = siteSetting.Mobile;
            model.Mobile2 = siteSetting.Mobile2;
            model.LogoName = siteSetting.LogoName;
            model.Address = siteSetting.Address;
            model.CollectionManagement = siteSetting.CollectionManagement;
            model.FooterDescription = siteSetting.FooterDescription;
            model.CopyrightDescription = siteSetting.CopyrightDescription;
            model.NewsletterDescription = siteSetting.NewsletterDescription;
            model.WithdrawRequestMinimumAmount = siteSetting.WithdrawRequestMinimumAmount;

            return model;
        }
    }
}
